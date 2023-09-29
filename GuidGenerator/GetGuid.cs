using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace GuidGenerator;

public class GetGuid
{
    private readonly ILogger _logger;

    public GetGuid(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GetGuid>();
    }

    // https://localhost:7094/api/GetGuid?count=10
    [Function("GetGuid")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Started the GetGuid Function Call");

        string? numberOfGuidsText = req.Query["count"];
        int numberOfGuids = 1;

        if (numberOfGuidsText is not null && int.TryParse(numberOfGuidsText, out numberOfGuids))
        {
            _logger.LogInformation($"Number of Guids requested: {numberOfGuids}");
        }
        else
        {
            _logger.LogInformation($"Unknown number of Guids requested. Using 1.");
            numberOfGuids = 1;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        
        return response;
    }
}
