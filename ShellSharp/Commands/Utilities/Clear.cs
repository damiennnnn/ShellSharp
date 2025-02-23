namespace ShellSharp.Commands.Utilities;

public class Clear : Utility
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