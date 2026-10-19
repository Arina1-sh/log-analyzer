using LogAnalyzer.App;

namespace LogAnalyzer.Tests;

public class UnitTest1
{
    [Fact]
    public void Parser_ValidLine_ReturnsEntry()
    {
        var parser = new LogParser();
        var line = "2026-10-19T21:50:59Z|INFO|App|123|Test message";
        
        var (entry, error) = parser.Parse(line);
        
        Assert.Null(error);
        Assert.NotNull(entry);
        Assert.Equal("INFO", entry.Level);
        Assert.Equal("App", entry.Source);
    }

    [Fact]
    public void Parser_InvalidLine_ReturnsError()
    {
        var parser = new LogParser();
        var line = "invalid line without pipes";
        
        var (entry, error) = parser.Parse(line);
        
        Assert.NotNull(error);
        Assert.Null(entry);
    }

    [Fact]
    public void Filter_MatchesCorrectly()
    {
        var filter = new LogFilter { Level = "ERROR" };
        var entry = new LogAnalyzer.App.Models.LogEntry { Level = "ERROR" };
        var entry2 = new LogAnalyzer.App.Models.LogEntry { Level = "INFO" };
        
        Assert.True(filter.Matches(entry));
        Assert.False(filter.Matches(entry2));
    }
}
