namespace ZANECO.API.Infrastructure.Logging;

public class LoggerSettings
{
    public string AppName { get; set; } = "ZANECO.API";
    public string ElasticSearchUrl { get; set; } = default!;
    public bool WriteToFile { get; set; } = default!;
    public bool StructuredConsoleLogging { get; set; } = default!;
    public string MinimumLogLevel { get; set; } = "Information";
}