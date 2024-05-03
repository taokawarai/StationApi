namespace StationApi
{
    public class ApiResponse
    {
        public Response? response { get; set; }
    }

    public class Response
    {
        public List<Station>? station { get; set; }
    }

    public class Station
    {
        public string? name { get; set; }
        public string? prefecture { get; set; }
        public string? line { get; set; }
        public float? x { get; set; }
        public float? y { get; set; }
        public string? postal { get; set; }
        public string? distance { get; set; }
        public string? prev { get; set; }
        public string? next { get; set; }
    }
}
