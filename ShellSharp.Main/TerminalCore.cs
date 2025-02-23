using System.Diagnostics;
using System.Reflection;
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
        "[#005faf]\uE0B0[/]" +
        " "
    );

    private string GetPrefix()
    {
        return CurrentPath.Get();
    }

    private void LoadConfigFromCurrentDirectory()
    {
        var configPath = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.TopDirectoryOnly)
            .FirstOrDefault(x => Path.GetFileName(x) == "shellsharp.config.json");
        
        if (configPath is not null)
            LoadConfig(configPath);
    }
    
    private bool LoadConfig(string configPath)
    {
        try
        {
            string configText = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<TerminalConfig>(configText);

            if (config is not null)
            {
                Config = config;
                return true;
            }
        }
        catch (IOException e)
        {
            Debug.WriteLine($"File error: {e.Message}");
        }
        catch (JsonException e)
        {
            Debug.WriteLine($"JSON error: {e.Message}");
        }

        return false;
    }
    
    private void Initialise()
    {
        LoadConfigFromCurrentDirectory();
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

        if (args[0] == "shellsharp")
        {
            // Terminal core config utilities
            if (commandArguments is null)
            {
                // No argument(s) provided, do default behaviour (help info)

                var helpInfo = new Markup(
                    $"ShellSharp Version: {Assembly.GetExecutingAssembly().GetName().Version}"
                    + Environment.NewLine
                    + """
                      
                      [b]config[/] - Manages ShellSharp Configuration
                      
                            config load [i]path[/] - Load configuration file from specified path.
                            config save [i]path[/] - Save current configuration file to specified path.
                      
                      """
                    );
                
                AnsiConsole.Write(helpInfo);
                return;
            }
            
            switch (commandArguments[0])
            {
                case "version": break;
                case "config":
                {
                    if (commandArguments.ElementAtOrDefault(1) is string configArg)
                    {
                        if (commandArguments.ElementAtOrDefault(2) is string configPath)
                        {
                            switch (configArg)
                            {
                                case "load":
                                    LoadConfig(configPath); break;
                                case "save": break;
                            } 
                        }
                        else
                        {
                            // Do other config stuff (change config from command-line)
                        }
                    }
                }
                    break;
            }

        }
        else if (Utilities.FindAndRun(commandMain, commandArguments))
        {
            // Search through our built-in utils for command
        }
        
    }
}