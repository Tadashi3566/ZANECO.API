namespace ZANECO.API.Application.Dashboard;

public class StatsDto
{
    public int UserCount { get; set; }
    public int RoleCount { get; set; }

    public int ProductCount { get; set; }
    public int BrandCount { get; set; }

    public int MemberCount { get; set; }
    public int AccountCount { get; set; }

    public int ContactCount { get; set; }
    public int SMSLogCount { get; set; }
    public int SMSTemplateCount { get; set; }

    public List<ChartSeries> BarChartSMSPerMonth { get; set; } = new();
    public List<ChartSeries> BarChartSMSPerDay { get; set; } = new();
    public List<ChartSeries> BarChartSandurot { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
}

public class ChartSeries
{
    public string? Name { get; set; }
    public double[]? Data { get; set; }
}