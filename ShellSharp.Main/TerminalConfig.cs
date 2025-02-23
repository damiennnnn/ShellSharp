namespace ShellSharp.Main;

public class TerminalConfig
{
    public string SegmentOneForeground { get; set; }
    public string SegmentOneBackground { get; set; }
    public string SegmentTwoForeground { get; set; }
    public string SegmentTwoBackground { get; set; }

    public TerminalConfig()
    {
        // Configure with default values
        
        SegmentOneForeground = "white";
        SegmentOneBackground = "#5f5f5f";
        SegmentTwoForeground = "white";
        SegmentTwoBackground = "#005faf";
    }
}