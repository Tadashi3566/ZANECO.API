﻿namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateExportDto : IDto
{
    public string RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}