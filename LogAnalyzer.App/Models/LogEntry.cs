namespace LogAnalyzer.App.Models;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string EventCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

