using LogAnalyzer.App.Models;

namespace LogAnalyzer.App;

public class LogFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Level { get; set; }
    public string? Source { get; set; }

    public bool Matches(LogEntry entry)
    {
        if (From.HasValue && entry.Timestamp < From.Value) return false;
        if (To.HasValue && entry.Timestamp >= To.Value) return false;
        if (!string.IsNullOrEmpty(Level) && !entry.Level.Equals(Level, StringComparison.OrdinalIgnoreCase)) return false;
        if (!string.IsNullOrEmpty(Source) && !entry.Source.Equals(Source, StringComparison.OrdinalIgnoreCase)) return false;
        return true;
    }
}

