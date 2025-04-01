

using System;
using System.Collections.Generic;
using System.IO;
using Tonga.Func;

namespace Tonga.Tests;

internal class Tidy : IAction
{
    private readonly Action act;
    private readonly IEnumerable<Uri> files;

    public Tidy(Action act, params Uri[] files)
    {
        this.files = files;
        this.act = act;
    }

    public void Invoke()
    {
        Delete();
        try
        {
            act.Invoke();
        }
        finally
        {
            Delete();
        }
    }

    private void Delete()
    {
        new Each<Uri>((uri) =>
            {
                if (File.Exists(uri.AbsolutePath)) File.Delete(uri.AbsolutePath);
            },
            this.files
        ).Invoke();

    }
}
