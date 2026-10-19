using System.Globalization;
using LogAnalyzer.App.Models;

namespace LogAnalyzer.App;

public class LogParser
{
    private static readonly HashSet<string> ValidLevels = new() { "TRACE", "DEBUG", "INFO", "WARN", "ERROR" };

    public (LogEntry? Entry, string? Error) Parse(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return (null, "Пустая строка");

        var parts = line.Split('|');
        if (parts.Length != 5)
            return (null, $"Ожидалось 5 полей, найдено {parts.Length}");

        if (!DateTime.TryParse(parts[0].Trim(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var timestamp))
            return (null, "Неверный формат даты (ожидается ISO 8601 UTC)");

        var level = parts[1].Trim().ToUpperInvariant();
        if (!ValidLevels.Contains(level))
            return (null, $"Неверный уровень: {parts[1]}");

        return (new LogEntry
        {
            Timestamp = timestamp,
            Level = level,
            Source = parts[2].Trim(),
            EventCode = parts[3].Trim(),
            Message = parts[4].Trim()
        }, null);
    }
}

