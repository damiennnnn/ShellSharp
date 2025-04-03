using System.Diagnostics;
using System.Text.Json;
using Spectre.Console;

// ReSharper disable ConvertTypeCheckPatternToNullCheck

namespace ShellSharp.Main;

public class TerminalCore
{
    public TerminalConfig Config { get; set; } = new();

    private TerminalHandler TerminalHandler { get; } = new();

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
            var configText = File.ReadAllText(configPath);
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

        TerminalHandler.Prompt = Prompt;
        TerminalHandler.RegisterBuiltInUtils();
    }

    public void Run()
    {
        Initialise();

        var exit = false;

        // Initial prompt
        AnsiConsole.Write(Prompt);

        // I/O loop
        do
        {
            TerminalHandler.HandleInput(Console.ReadKey(false));
        } while (!exit);
    }
}