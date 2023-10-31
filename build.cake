#tool nuget:?package=GitReleaseManager
#tool nuget:?package=OpenCover
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Incubator&version=8.0.0

var target                  = Argument("target", "Default");
var configuration           = "Release";

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var buildArtifacts          = Directory("./artifacts");
var deployment              = Directory("./artifacts/deployment");
var version                 = "0.1.0";

///////////////////////////////////////////////////////////////////////////////
// MODULES
///////////////////////////////////////////////////////////////////////////////
var modules                 = Directory("./src");
var blacklistedModules      = new List<string>() { };

var unitTests               = Directory("./tests");
var blacklistedUnitTests    = new List<string>() { }; 

///////////////////////////////////////////////////////////////////////////////
// CONFIGURATION VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isAppVeyor              = AppVeyor.IsRunningOnAppVeyor;
var isWindows               = IsRunningOnWindows();

// For GitHub release
var owner                   = "NACLWARE";
var repository              = "Tonga";

// For NuGetFeed
var nuGetSource             = "https://api.nuget.org/v3/index.json";
var appVeyorNuGetFeed       = "https://ci.appveyor.com/nuget/icarus/api/v2/package";

// API key tokens for deployment
var nugetReleaseToken       = "";

///////////////////////////////////////////////////////////////////////////////
// Version
///////////////////////////////////////////////////////////////////////////////
Task("Version")
.WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.Does(() => 
{
    version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
    Information($"Set version to '{version}'");
});

///////////////////////////////////////////////////////////////////////////////
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
.Does(() =>
{   
    CleanDirectories(new DirectoryPath[] { buildArtifacts });
    foreach(var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            CleanDirectories(
                new DirectoryPath[] 
                { 
                    $"{module}/bin",
                    $"{module}/obj",
                }
            );
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Restore
///////////////////////////////////////////////////////////////////////////////
Task("Restore")
.Does(() =>
{
    NuGetRestore($"./{repository}.sln");
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.Does(() =>
{
    var settings = 
        new DotNetBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            MSBuildSettings = new DotNetMSBuildSettings().SetVersionPrefix(version)
        };
        var skipped = new List<string>();
    foreach(var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            Information($"Building {name}");
            
            DotNetBuild(
                module.FullPath,
                settings
            );
        }
        else
        {
            skipped.Add(name);
        }
    }
    if (skipped.Count > 0)
    {
        Warning("The following builds have been skipped:");
        foreach(var name in skipped)
        {
            Warning($"  {name}");
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Unit Tests
///////////////////////////////////////////////////////////////////////////////
Task("UnitTests")
.IsDependentOn("Build")
.Does(() => 
{
    var settings = 
        new DotNetTestSettings()
        {
            Configuration = configuration,
            NoRestore = true
        };
    var skipped = new List<string>();   
    foreach(var test in GetSubDirectories(unitTests))
    {
        var name = test.GetDirectoryName();
        if(blacklistedUnitTests.Contains(name))
        {
            skipped.Add(name);
        }
        else if(!name.StartsWith("TmxTest"))
        {
            Information($"Testing {name}");
            DotNetTest(
                test.FullPath,
                settings
            );
        }
    }
    if (skipped.Count > 0)
    {
        Warning("The following tests have been skipped:");
        foreach(var name in skipped)
        {
            Warning($"  {name}");
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Generate Coverage
///////////////////////////////////////////////////////////////////////////////
Task("GenerateCoverage")
.IsDependentOn("Build")
.Does(() => 
{
    try
    {
        OpenCover(
            tool => 
            {
                tool.DotNetTest(
                    "./tests/Yaapii.Atoms.Tests/",
                    new DotNetTestSettings
                    {
                        Configuration = configuration
                    }
                );
            },
            new FilePath($"{buildArtifacts.Path}/coverage.xml"),
            new OpenCoverSettings()
            {
                OldStyle = true
            }.WithFilter("+[Yaapii.Atoms]*")
        );
    }
    catch(Exception ex)
    {
        Information("Error: " + ex.ToString());
    }
});

///////////////////////////////////////////////////////////////////////////////
// Assert Packages
///////////////////////////////////////////////////////////////////////////////
Task("AssertPackages")
.Does(() => 
{
    foreach (var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            var project = ParseProject(new FilePath($"{module}/{name}.csproj"), configuration);
            var packageVersion = new Dictionary<string, string>();
            foreach (var package in project.PackageReferences)
            {
                packageVersion.Add(package.Name, package.Version);
            }

            foreach (var package in packageVersion)
            {
                if (package.Key.Contains(".Sources"))
                {
                    var nonSourcesPackage = package.Key.Replace(".Sources", string.Empty);
                    if (packageVersion[nonSourcesPackage] != package.Value)
                    {
                        throw new Exception(
                            $"Reference nuget packages must have equal version in project {name}:{Environment.NewLine}"
                            + $"\t{package.Key} {package.Value} and {nonSourcesPackage} {packageVersion[nonSourcesPackage]}.{Environment.NewLine}"
                            + $"\tUpdate nuget package in the {name}.csproj file.{Environment.NewLine}"
                            + $"\tHint: search for '<PackageReference Include=\"{package.Key}\" Version=\"{package.Value}\" Condition=\"'$(Configuration)' == 'ReleaseSources'\">'."
                        );    
                    }
                }
            }
        }
    }
    Information("Package validation passed.");
});

///////////////////////////////////////////////////////////////////////////////
// NuGet
///////////////////////////////////////////////////////////////////////////////
Task("NuGet")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("AssertPackages")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.Does(() =>
{
    Information($"Building NuGet Package for Version {version}");
    
    var settings = new DotNetPackSettings()
    {
        Configuration = configuration,
        OutputDirectory = buildArtifacts,
        NoRestore = true
    };
    settings.ArgumentCustomization = args => args.Append("--include-symbols").Append("-p:SymbolPackageFormat=snupkg");
    settings.MSBuildSettings = new DotNetMSBuildSettings().SetVersionPrefix(version);

    var settingsSources = new DotNetPackSettings()
    {
        Configuration = "ReleaseSources",
        OutputDirectory = buildArtifacts,
        NoRestore = false,
        NoBuild = false,
        VersionSuffix = ""
    };
    settingsSources.MSBuildSettings = new DotNetMSBuildSettings().SetVersionPrefix(version);

    foreach (var module in GetSubDirectories(modules))
    {
        var name = module.GetDirectoryName();
        if(!blacklistedModules.Contains(name))
        {
            DotNetPack(
                module.ToString(),
                settings
            );

            settingsSources.ArgumentCustomization = args => args.Append($"-p:PackageId={name}.Sources").Append("-p:IncludeBuildOutput=false");
            DotNetPack(
                module.ToString(),
                settingsSources
            );
        }
        else
        {
            Warning($"Skipping NuGet package for {name}");
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Credentials
///////////////////////////////////////////////////////////////////////////////
Task("Credentials")
.WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.Does(() =>
{
    nugetReleaseToken = EnvironmentVariable("NUGET_TOKEN");
    if (string.IsNullOrEmpty(nugetReleaseToken))
    {
        throw new Exception("Environment variable 'NUGET_TOKEN' is not set");
    }
});

///////////////////////////////////////////////////////////////////////////////
// NuGet Feed
///////////////////////////////////////////////////////////////////////////////
Task("NuGetFeed")
.WithCriteria(() => isAppVeyor && BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.IsDependentOn("NuGet")
.IsDependentOn("Credentials")
.Does(() => 
{
    var nugets = GetFiles($"{buildArtifacts.Path}/*.nupkg");
    foreach(var package in nugets)
    {
        NuGetPush(
            package,
            new NuGetPushSettings {
                Source = nuGetSource,
                ApiKey = nugetReleaseToken
            }
        );
    }
    var symbols = GetFiles($"{buildArtifacts.Path}/*.snupkg");
    foreach(var symbol in symbols)
    {
        NuGetPush(
            symbol,
            new NuGetPushSettings {
                Source = nuGetSource,
                ApiKey = nugetReleaseToken
            }
        );
    }
});

///////////////////////////////////////////////////////////////////////////////
// Default
///////////////////////////////////////////////////////////////////////////////
Task("Default")
.IsDependentOn("Credentials")
.IsDependentOn("Version")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.IsDependentOn("UnitTests")
.IsDependentOn("AssertPackages")
.IsDependentOn("NuGet")
.IsDependentOn("NuGetFeed");

RunTarget(target);
