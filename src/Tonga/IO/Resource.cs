

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tonga.IO.Error;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// <para>An embedded resource.</para>
    /// </summary>
    public class Resource(string name, Func<Assembly> container, IEnumerable<char> symbols) : IConduit
    {
        private readonly Lazy<Assembly> assembly = new(container);

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="type">a class that is in the same container (assembly) with the resource</param>
        public Resource(string name, Type type) : this(
            name,
            () => Assembly.GetAssembly(type)
        )
        { }

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="container">container to search in. Use Assembly.GetExecutingAssembly() for the assembly your current code is in.</param>
        public Resource(string name, Assembly container) : this(
            name,
            () => container
        )
        { }

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="container">container to search in. Use Assembly.GetExecutingAssembly() for the assembly your current code is in.</param>
        public Resource(string name, Func<Assembly> container) : this(
            name,
            container,
            new List<char>()
            {
                '!', '§', '$', '=', '+', '-', '(', ')', '[', ']', '{', '}', ';', ',', '`', '´', '\''
            }
        )
        { }

        /// <summary>
        /// Stream of the resource.
        /// </summary>
        /// <exception cref = "ResourceNotFoundException" >if resource is not present</exception >
        /// <returns>stream of the resource</returns>
        public Stream Stream()
        {
            var asm = assembly.Value;
            var fullName = FullName();
            var s = asm.GetManifestResourceStream(fullName);

            if (s == null)
            {
                throw new ResourceNotFoundException(fullName, asm);
            }

            return s;
        }

        private string FullName()
        {
            var folders = name.Split('\\', '/');
            for (int current = 0; current < folders.Length - 1; current++)
            {
                folders[current] =
                    SymbolsEndoced(
                        NumbersEncoded(
                            DotsEncoded(folders[current])
                        )
                    );
            }
            return assembly.Value.GetName().Name + "." + String.Join(".", folders);
        }

        /// <summary>
        /// Replace all occurence of '.' by "._"
        /// Example: "_version8.1" -> "_version8._1"
        /// </summary>
        /// <param name="path">The path to translate</param>
        /// <returns>Result</returns>
        private string DotsEncoded(string path)
        {
            var pieces = path.Split('.');
            for (int idx = 1; idx < pieces.Length; idx++)
            {
                if (pieces[idx].StartsWith("_"))
                {
                    pieces[idx] = pieces[idx].Substring(1);
                }
            }
            path = String.Join("._", pieces);
            return path;
        }

        /// <summary>
        /// When the folder begins with a digit it will be amended by a leading '_'.
        /// Example: "7text7" -> "_7text7"
        /// </summary>
        /// <param name="path">The path to translate</param>
        /// <returns>Result</returns>
        private string NumbersEncoded(string path)
        {
            if (path.Length > 0)
            {
                var first = path.Substring(0, 1).ToCharArray()[0];
                if (first >= '0' && first <= '9')
                {
                    path = "_" + path;
                }
            }
            return path;
        }

        /// <summary>
        /// Replaces all symbols by '_'.
        /// When the path is equal to that symbol is will be replaced by "__".
        /// Example: "unique-name" -> "unique_name"
        /// Example: "{" -> "__"
        /// </summary>
        /// <param name="path">The path to translate</param>
        /// <returns>Result</returns>
        private string SymbolsEndoced(string path)
        {
            var encoded = path;
            foreach (var symbol in symbols)
            {
                if (encoded.Equals(symbol.ToString()))
                    encoded = "__";
                encoded = encoded.Replace(symbol, '_');
            }
            return encoded;
        }
    }
}
