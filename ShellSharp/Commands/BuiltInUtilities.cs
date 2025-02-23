using ShellSharp.Commands.Utilities;

namespace ShellSharp.Commands;

public class BuiltInUtilities
{
    private readonly List<Utility> _utils =
    [
        new ChangeDirectory(),
        new ListDirectory(),
        new Clear()
    ];

    public bool FindAndRun(string cmd, string[]? args)
    {
        var utility = _utils.Find(u => u.CommandName == cmd);
        if (utility is null)
            return false;

        // We found a match, call util
        utility.Handle(args);
        return true;
    }
}