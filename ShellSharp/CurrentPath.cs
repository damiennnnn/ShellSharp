namespace ShellSharp;

public static class CurrentPath
{
    private static string _path;

    static CurrentPath()
    {
        _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }

    public static string Get()
    {
        return _path;
    }

    public static bool Set(string path)
    {
        return Directory.Exists(path)
               && (_path = path) != null;
    }
}