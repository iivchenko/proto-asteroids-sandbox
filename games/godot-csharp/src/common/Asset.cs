using Godot;
using System;
using System.Linq;

public static partial class Asset
{
    public static string RandomAsset(string path)
    {
        var files =
            DirAccess
                .Open(path)
                .GetFiles()
                .Where(x => x.EndsWith(".import"))
                .Select(x => x.Replace(".import", string.Empty));

        return path + Random.Shared.Pick(files);
    }
}
