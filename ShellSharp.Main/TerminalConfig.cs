namespace ShellSharp.Main;

public class TerminalConfig
{
    public TerminalConfig()
    {
        // Configure with default values

        SegmentOneForeground = "white";
        SegmentOneBackground = "#5f5f5f";
        SegmentTwoForeground = "white";
        SegmentTwoBackground = "#005faf";
    }

    public string SegmentOneForeground { get; set; }
    public string SegmentOneBackground { get; set; }
    public string SegmentTwoForeground { get; set; }
    public string SegmentTwoBackground { get; set; }
}