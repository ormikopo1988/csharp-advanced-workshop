using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetCoreWebApiDemo.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }

        [JsonPropertyName("coord")]
        public Coordinate Coordinate { get; set; } = default!;

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();

        [JsonPropertyName("main")]
        public Main Main { get; set; } = default!;

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; } = default!;
    }

    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; } = default!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
    }

    public class Coordinate
    {
        [JsonPropertyName("lon")]
        public float Lon { get; set; }

        [JsonPropertyName("lat")]
        public float Lat { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public float Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public float FeelsLike { get; set; }

        [JsonPropertyName("temp_min")]
        public float TempMin { get; set; }

        [JsonPropertyName("temp_max")]
        public float TempMax { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public float Speed { get; set; }

        [JsonPropertyName("deg")]
        public int Degree { get; set; }

        [JsonPropertyName("gust")]
        public float Gust { get; set; }
    }
}
