namespace ShellSharp.Commands.Utilities;

public class Clear : Command
{
    public Clear()
    {
        FriendlyName = "Clear Console";
        CommandName = "clear";
    }

    public override void Handle(string[]? args)
    {
        Console.Clear();
    }
}