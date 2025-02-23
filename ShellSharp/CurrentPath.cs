namespace ShellSharp;

public class CurrentPath
{
    private string _path;
    public string Get() => _path;

    public CurrentPath()
    {
        _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
    
    public bool Set(string path)
    {
        return Directory.Exists(path) 
               && (_path = path) != null;
    }
}