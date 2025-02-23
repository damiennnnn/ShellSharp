using System.Diagnostics;
using System.Text.Json;
using ShellSharp.Commands;
using Spectre.Console;
// ReSharper disable ConvertTypeCheckPatternToNullCheck

namespace ShellSharp.Main;

public class TerminalCore
{
    private BuiltInUtilities Utilities { get; } = new();
    public TerminalConfig Config { get; set; } = new();
    private Markup Prompt => new(
        $"[{Config.SegmentOneForeground} on {Config.SegmentOneBackground}]" +
        $" {Environment.UserName} [/]" +
        $"[{Config.SegmentOneBackground} on {Config.SegmentTwoBackground}]" +
        "\uE0B0[/]" +
        $"[{Config.SegmentTwoForeground} on {Config.SegmentTwoBackground}]" +
        $"{GetPrefix()}[/]" +
        "[#005faf]\uE0B0[/]"
    );

    private string GetPrefix()
    {
        return CurrentPath.Get();
    }

    private void LoadConfig()
    {
        // Look for config.json, otherwise create it.
        var configPath = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.TopDirectoryOnly)
            .FirstOrDefault(x => Path.GetFileName(x) == "shellsharp.config.json");
        
        if (configPath is not null && File.ReadAllText(configPath) is string configText)
        {
            try
            {
                var config = JsonSerializer.Deserialize<TerminalConfig>(configText);
                if (config is not null)
                    Config = config;
            }
            catch (JsonException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
    
    private void Initialise()
    {
        LoadConfig();
    }
    
    public void Run()
    {
        Initialise();
        
        var exit = false;
        do
        {
            AnsiConsole.Write(Prompt);
            while (Console.ReadLine() is string cmd)
            {
                Interpret(cmd);

                exit = cmd == "exit";
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
        var commandArguments = args.Length > 1 ? args[1..] : default;

        // Search through our built-in utils for command
        if (Utilities.FindAndRun(commandMain, commandArguments))
        {
        }
    }
}