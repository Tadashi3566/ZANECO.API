// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ZANECO.API.Application.Common.Interfaces;
using ZANECO.API.Application.Common.Persistence;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.PaddleOCR;

public class DocumentOcrJob : IDocumentOcrJob
{
    private readonly IRepositoryWithEvents<Document> _repository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISerializerService _serializer;
    private readonly ILogger<Document> _logger;
    private readonly Stopwatch _timer;

    public DocumentOcrJob(
        IRepositoryWithEvents<Document> repository,
        IHttpClientFactory httpClientFactory,
        ISerializerService serializer,
        ILogger<Document> logger)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
        _serializer = serializer;
        _logger = logger;
        _timer = new Stopwatch();
    }

    private string Readbase64string(string path)
    {
        using var image = Image.FromFile(path);
        using var m = new MemoryStream();

        image.Save(m, image.RawFormat);
        byte[] imageBytes = m.ToArray();

        // Convert byte[] to Base64 String
        return Convert.ToBase64String(imageBytes);
    }

    public async Task Recognition(DefaultIdType id, CancellationToken cancellationToken)
    {
        try
        {
            _timer.Start();

            var document = await _repository.GetByIdAsync(id, cancellationToken);
            if (document is null) return;

            if (string.IsNullOrEmpty(document.ImagePath)) return;

            string imgfile = Path.Combine(Directory.GetCurrentDirectory(), document.ImagePath);

            if (!File.Exists(imgfile)) return;

            string base64string = Readbase64string(imgfile);

            using var client = _httpClientFactory.CreateClient("ocr");
            var response = client.PostAsJsonAsync<dynamic>(string.Empty, new { images = new string[] { base64string } }, cancellationToken: cancellationToken).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync(cancellationToken);
                var ocrResult = JsonSerializer.Deserialize<Ocr_result>(result);

                string ocr_status = ocrResult!.status;
                if (ocrResult?.status == "000")
                {
                    StringBuilder documentContent = new();
                    var documentDate = DateTime.MinValue;

                    foreach (var results in ocrResult.results)
                    {
                        if (results.Length > 0)
                        {
                            foreach (var array in results)
                            {
                                documentContent.Append(array.text).Append(Environment.NewLine);
                            }
                        }
                    }

                    var updatedDocument = document.UpdateContent(documentContent.ToString(), _serializer.Serialize(ocrResult.results));
                    await _repository.UpdateAsync(updatedDocument, cancellationToken);
                }

                _timer.Stop();

                long elapsedMilliseconds = _timer.ElapsedMilliseconds;

                _logger.LogInformation("Id: {id}, elapsed: {elapsedMilliseconds}, recognize the result: {@result},{@content}", id, elapsedMilliseconds, ocrResult, document.Content);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{id}: recognize error {ex.Message}");
        }
    }
}

internal class Result
{
    public decimal confidence { get; set; }
    public string text { get; set; } = default!;
    public List<int[]> text_region { get; set; } = new();
}

internal class Ocr_result
{
    public string msg { get; set; } = default!;
    public List<Result[]> results { get; set; } = new();
    public string status { get; set; } = default!;
}