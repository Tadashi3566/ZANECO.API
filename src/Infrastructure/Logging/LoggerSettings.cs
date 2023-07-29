namespace ZANECO.API.Infrastructure.Logging;

public class LoggerSettings
{
    public string AppName { get; set; } = "ZANECO.API";
    public string ElasticSearchUrl { get; set; } = default!;
    public bool WriteToFile { get; set; } = false;
    public bool StructuredConsoleLogging { get; set; } = false;
    public string MinimumLogLevel { get; set; } = "Information";
}