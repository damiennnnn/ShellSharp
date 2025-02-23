namespace ShellSharp.Commands.Utilities;

public class ListDirectory : Utility
{
    public ListDirectory()
    {
        FriendlyName = "List Directory";
        CommandName = "ls";
    }

    public override void Handle(string[]? args)
    {
        var dir = args == null || args.Length == 0
            ? CurrentPath.Get()
            : args[0];

        if (!Directory.Exists(dir)) return;

        // Get all file & directory names
        var files = Directory.GetFiles(dir)
            .Union(Directory.GetDirectories(dir))
            .Select(Path.GetFileName);

        foreach (var file in files) Console.WriteLine(file);
    }
}