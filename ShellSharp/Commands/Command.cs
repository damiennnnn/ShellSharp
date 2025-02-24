namespace ShellSharp.Commands;

public class Command
{
    public string FriendlyName { get; set; }
    public string CommandName { get; set; }
    public string Description { get; set; }

    public virtual void Handle(string[]? args)
    {
        // Do nothing by default
    }

    public virtual void AutoComplete(string[]? args)
    {
        // Do nothing by default
    }
}