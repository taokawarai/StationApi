using Microsoft.AspNetCore.Mvc;

namespace StationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClosestStationController : ControllerBase
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly ILogger<ClosestStationController> _logger;

        public ClosestStationController(ILogger<ClosestStationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetNearestStation")]
        public async Task<Station?> Get([FromQuery] float x, [FromQuery] float y)
        {
            // https://express.heartrails.com/api.html
            var response = await _httpClient.GetAsync($"https://express.heartrails.com/api/json?method=getStations&x={x}&y={y}");

            // https://learn.microsoft.com/ja-jp/dotnet/fundamentals/networking/http/httpclient#http-valid-content-responses
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return SearchClosestStation(content);
        }

        private Station? SearchClosestStation(ApiResponse? content)
        {
            if (content?.response?.station?.Count < 1) return new Station();
            var closestStation = content?.response?.station?[0];

            foreach (var station in content?.response?.station ?? new List<Station>())
            {
                // "100m" -> 100
                int.TryParse(closestStation?.distance?.Substring(0, closestStation.distance.Length - 1), out int closestStationDistance);
                int.TryParse(station?.distance?.Substring(0, station.distance.Length - 1), out int stationDistance);

                if (stationDistance < closestStationDistance) closestStation = station;
            }

            return closestStation;
        }
    }
}
