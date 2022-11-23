using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OpenMateo 
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<int> relativehumidity_2m { get; set; }
        public List<double> rain { get; set; }
        public List<int> weathercode { get; set; }
        public List<int> cloudcover { get; set; }
        public List<double> windspeed_10m { get; set; }
        public List<int> winddirection_10m { get; set; }
        public List<double> soil_temperature_0cm { get; set; }
        public List<double> soil_moisture_0_1cm { get; set; }
        public List<double> shortwave_radiation { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string rain { get; set; }
        public string relativehumidity_2m { get; set; }
        public string weathercode { get; set; }
        public string cloudcover { get; set; }
        public string windspeed_10m { get; set; }
        public string winddirection_10m { get; set; }
        public string soil_temperature_0cm { get; set; }
        public string soil_moisture_0_1cm { get; set; }
        public string shortwave_radiation { get; set; }
    }

    public class Root
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
    } 
}
