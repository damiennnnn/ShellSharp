using ShellSharp.Commands;
using ShellSharp.Commands.Utilities;
using Spectre.Console;

namespace ShellSharp.Main;

public class TerminalHandler
{
    public Markup Prompt { get; set; }
    
    private List<Command> Commands { get; } = new();
    private Stack<string> CommandHistory { get; } = new();
    private Stack<char> CurrentInput { get; set; } = new();
    private int HistoryIndex { get; set; } = 0;
    
    public void RegisterCommand(Command command)
    {
        Commands.Add(command);
    }
    
    public void RegisterBuiltInUtils()
    {
        RegisterCommand(new ChangeDirectory());
        RegisterCommand(new Clear());
        RegisterCommand(new ListDirectory());
    }

    public void HistoryPush(string command)
    {
        CommandHistory.Push(command);
    }

    public string HistoryUp()
    {
        return string.Empty;
    }

    public string HistoryDown()
    {
        return string.Empty;
    }

    public void HandleInput(ConsoleKeyInfo keyInfo) 
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow: break;
            case ConsoleKey.DownArrow: break;
            case ConsoleKey.Backspace:
                break;
            default:
                break;
        }
    }
    
    public bool Parse(string line)
    {
        // Push input to command history for Up/DownArrow actions
        CommandHistory.Push(line);
        
        var split = line.Split(' ');
        
        
        return false;
    }
    
    public bool FindAndExecute(string command, string[] args)
    {
        var cmd = Commands.Find(u => u.CommandName == command);
        if (cmd is null)
            return false;

        // We found a match, call util
        cmd.Handle(args);
        return true;
    }
}