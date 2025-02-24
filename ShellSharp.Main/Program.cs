using System.Reflection;
using ShellSharp.Main;

Console.WriteLine($"ShellSharp Version: {Assembly.GetExecutingAssembly().GetName().Version}");
var core = new TerminalCore();
core.Run();