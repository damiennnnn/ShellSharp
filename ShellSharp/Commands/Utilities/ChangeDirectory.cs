namespace ShellSharp.Commands.Utilities;

public class ChangeDirectory : Command
{
    public ChangeDirectory()
    {
        FriendlyName = "Change Directory";
        CommandName = "cd";
    }

    public override void Handle(string[]? args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("No directory specified.");
            return;
        }

        if (args[0] == "..")
        {
            var dir = Directory.GetParent(CurrentPath.Get());

            if (dir is not null)
                CurrentPath.Set(dir.FullName);

            return;
        }

        if (!CurrentPath.Set(args[0]))
        {
            // Check & get directory relative to current path
            var directory = Directory.GetDirectories(CurrentPath.Get())
                .Where(d => Path.GetFileName(d) == args[0])
                .FirstOrDefault(Directory.Exists);

            if (directory is null)
                Console.WriteLine("No directory found.");

            else
                CurrentPath.Set(directory);
        }
    }
}