using ShellSharp.Commands;

namespace ShellSharp.Main;

public class CommandLoop
{
    private BuiltInUtilities Utilities { get; } = new();

    private string GetPrefix()
    {
        return CurrentPath.Get();
    }
    
    public void Run()
    {
        bool exit = false;
        
        do
        {
            Console.Write($"{GetPrefix()} > ");

            while (Console.ReadLine() is string cmd)
            {
                Interpret(cmd);
                
                exit = (cmd == "exit");
                break;
            }
        } while (!exit);
    }

    private void Interpret(string command)
    {
        // Split input into command main and params
        var args = command.Split(' ');
        
        // The actual command
        var commandMain = args[0];
        string[]? commandArguments = (args.Length > 1) ? args[1..] : default;
        
        // Search through our built-in utils for command
        if (Utilities.FindAndRun(commandMain, commandArguments))
        {
            
        }
    }   
}