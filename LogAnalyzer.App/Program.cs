using LogAnalyzer.App;

var parser = new LogParser();
var filter = new LogFilter();
int totalLines = 0, validLines = 0, invalidLines = 0, matchedLines = 0;

string? inputPath = null;
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--input" && i + 1 < args.Length) inputPath = args[++i];
    if (args[i] == "--from" && i + 1 < args.Length) filter.From = DateTime.Parse(args[++i]);
    if (args[i] == "--to" && i + 1 < args.Length) filter.To = DateTime.Parse(args[++i]);
    if (args[i] == "--level" && i + 1 < args.Length) filter.Level = args[++i];
    if (args[i] == "--source" && i + 1 < args.Length) filter.Source = args[++i];
}

Console.WriteLine($"Отладка: путь к файлу = '{inputPath}'");
Console.WriteLine($"Отладка: файл существует? {File.Exists(inputPath)}");

if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
{
    Console.WriteLine("Ошибка: файл не найден или не указан путь (--input path)");
    return;
}

using var reader = new StreamReader(inputPath);
string? line;
while ((line = reader.ReadLine()) != null)
{
    totalLines++;
    var (entry, error) = parser.Parse(line);
    if (error != null)
    {
        invalidLines++;
        Console.WriteLine($"Строка {totalLines}: ОШИБКА - {error}");
        continue;
    }

    validLines++;
    if (filter.Matches(entry!))
    {
        matchedLines++;
    }
}

Console.WriteLine($"\n--- Сводка ---");
Console.WriteLine($"Всего строк: {totalLines}");
Console.WriteLine($"Корректных: {validLines}");
Console.WriteLine($"Ошибочных: {invalidLines}");
Console.WriteLine($"Прошло фильтр: {matchedLines}");
