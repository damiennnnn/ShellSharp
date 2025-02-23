using System.Reflection;
using System.Text.Json;
using ShellSharp.Main;

Console.WriteLine($"ShellSharp Version: {Assembly.GetExecutingAssembly().GetName().Version}");
var core = new TerminalCore();
core.Run();