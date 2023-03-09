using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System;
using UnityEngine.Events;

/// <summary>
/// ChangeWeatherEvent is listened by VirtualWeather class
/// To use this event it needs to added VirtualWeather game object in the game objects hierarchy 
/// to the inspector of OnChangeWeatherEvent on WebRequestAPI game object
/// </summary>
[System.Serializable] public class ChangeWeatherEvent : UnityEvent<string, int, double, double> { }

public class WebRequestController : Singleton<WebRequestController>
{
    private static string par_temperature = "temperature_2m";
    private static string par_humidity = "relativehumidity_2m";
    private static string par_wcode = "weathercode";
    private static string par_cloud = "cloudcover";
    private static string par_radiation = "shortwave_radiation";
    private static string par_wind = "windspeed_10m";
    private static string par_soil_temperature = "soil_temperature_0cm";
    private static string par_soil_moisture = "soil_moisture_0_1cm";
    private string urlOpenMeteo = "https://api.open-meteo.com/v1/forecast?latitude=41.80&longitude=-6.77&hourly="+par_temperature+","+par_humidity+","+par_wcode+","+par_cloud+","+par_radiation+","+par_wind+","+par_soil_temperature+","+par_soil_moisture;
	private OpenMateo.Root myDeserializedClass = null;
    private Dictionary<Code, string> WeatherStates =  null;
    private enum Code { Zero = 0, One = 1, Two = 2, Three = 3,  FortyFive = 45, FortyEight = 48,  FiftyOne = 51, FiftyThree = 53, FiftyFive = 55, FiftySix = 56, FiftySeven = 57, SixtyOne = 61, SixtyThree = 63, SixtyFive = 65, SixtySix = 66, SixtySeven = 67, SeventyOne = 71,  SeventyThree = 73,  SeventyFive = 75, SeventySeven = 77, Eighty = 80, EightyOne = 81, EightyTwo = 82, EightyFive = 85, EightySix = 86, NinetyFive = 95, NinetySix = 96, NinetyNine= 99 };
    public ChangeWeatherEvent OnChangeWeatherEvent;

    /// <summary>
    /// WeatherDescription is a list containing the description of WMO Weather interpretation codes (WW)
    /// translated to portuguese from https://open-meteo.com/en/docs
    /// </summary>
    private List<string> WeatherDescription = new List<string>()
    {
        "Céu limpo", //0
        "Parcialmente nublado e encoberto", //1
        "Nevoeiro e geada", //2
        "Garoa", //3
        "Garoa Congelante", //4
        "Chuva", //5
        "Chuva Congelante", //6
        "Queda de neve", //7
        "Grãos de neve", //8
        "Pancadas de chuva", //9
        "Aguaceiros de neve", //10
        "Trovoada", //11
        "Trovoada com granizo" //12
    };

    private void InitWeatherStates()
    {
        WeatherStates = new Dictionary<Code, string>();

        WeatherStates.Add(Code.Zero, WeatherDescription.ElementAt(0));

        WeatherStates.Add(Code.One, WeatherDescription.ElementAt(1));
        WeatherStates.Add(Code.Two,  WeatherDescription.ElementAt(1));
        WeatherStates.Add(Code.Three,  WeatherDescription.ElementAt(1));
        
        WeatherStates.Add(Code.FortyFive, WeatherDescription.ElementAt(2));
        WeatherStates.Add(Code.FortyEight, WeatherDescription.ElementAt(2));

        WeatherStates.Add(Code.FiftyOne, WeatherDescription.ElementAt(3));
        WeatherStates.Add(Code.FiftyThree, WeatherDescription.ElementAt(3));
        WeatherStates.Add(Code.FiftyFive, WeatherDescription.ElementAt(3));

        WeatherStates.Add(Code.FiftySix, WeatherDescription.ElementAt(4));
        WeatherStates.Add(Code.FiftySeven, WeatherDescription.ElementAt(4));

        WeatherStates.Add(Code.SixtyOne, WeatherDescription.ElementAt(5));
        WeatherStates.Add(Code.SixtyThree, WeatherDescription.ElementAt(5));
        WeatherStates.Add(Code.SixtyFive, WeatherDescription.ElementAt(5));

        WeatherStates.Add(Code.SixtySix, WeatherDescription.ElementAt(6));
        WeatherStates.Add(Code.SixtySeven, WeatherDescription.ElementAt(6));

        WeatherStates.Add(Code.SeventyOne, WeatherDescription.ElementAt(7));
        WeatherStates.Add(Code.SeventyThree, WeatherDescription.ElementAt(7));
        WeatherStates.Add(Code.SeventyFive, WeatherDescription.ElementAt(7));

        WeatherStates.Add(Code.SeventySeven, WeatherDescription.ElementAt(8));

        WeatherStates.Add(Code.Eighty, WeatherDescription.ElementAt(9));
        WeatherStates.Add(Code.EightyOne, WeatherDescription.ElementAt(9));
        WeatherStates.Add(Code.EightyTwo, WeatherDescription.ElementAt(9));

        WeatherStates.Add(Code.EightyFive, WeatherDescription.ElementAt(10));
        WeatherStates.Add(Code.EightySix, WeatherDescription.ElementAt(10));

        WeatherStates.Add(Code.NinetyFive, WeatherDescription.ElementAt(11));

        WeatherStates.Add(Code.NinetySix, WeatherDescription.ElementAt(12));
        WeatherStates.Add(Code.NinetyNine, WeatherDescription.ElementAt(12));
    }

    void Start()
    {
       InitWeatherStates();
       // Debug.Log("values: " + WeatherStates[Code.FortyFive] + " num: " + (int)Code.FortyFive);
       // Debug.Log("values2: " + WeatherStates.ElementAt(0).Value ); // Bar
        StartCoroutine(GetFromOpenMateoAPI(urlOpenMeteo));
    }

    private IEnumerator GetFromOpenMateoAPI(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri); 
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log("Error while Receiving: " + www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            myDeserializedClass = JsonConvert.DeserializeObject<OpenMateo.Root>(result);
            GetValuesRequest();
        }
    }

    private void GetValuesRequest()
    {             
        int i = 0;
        foreach (string y in myDeserializedClass.hourly.time)
        {
            //Spliting date received 2022-11-06 T 14:00
            string[] dateHour = y.Split('T'); 
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string currentHour = dt.ToString("HH");
            string customDate = dt.ToShortDateString();
            customDate = dt.ToString("yyyy/MM/dd");
                                            
            if (customDate == dateHour[0].Replace('-', '/'))
            {
                var temp = myDeserializedClass.hourly.temperature_2m.ElementAt(i);
                var humidity = myDeserializedClass.hourly.relativehumidity_2m.ElementAt(i);
                var weathercode = myDeserializedClass.hourly.weathercode.ElementAt(i);
                var cloud = myDeserializedClass.hourly.cloudcover.ElementAt(i);
                var wind = myDeserializedClass.hourly.windspeed_10m.ElementAt(i);
                var radiation = myDeserializedClass.hourly.shortwave_radiation.ElementAt(i);
                var soilTemp = myDeserializedClass.hourly.soil_temperature_0cm.ElementAt(i);
                var soilHum = myDeserializedClass.hourly.soil_moisture_0_1cm.ElementAt(i);

                //Debug.Log("Date: " + dateHour[0] + " Hour: " + dateHour[1] + " Temp: " + temp + " Rain: " + humidity + " Cloud: " + cloud + " weather_code: " + weathercode);//+ "ºC");
                //Debug.Log("Agora são: " + currentHour + " datahour: " + dateHour[1]);

                currentHour += ":00";
                if (currentHour == dateHour[1])
                {
                    string currentWeather = SearchWeatherStateByCode(weathercode);
                    //Debug.Log("Agora são: " + currentHour);
                    //Debug.Log("Date: " + dateHour[0] + " Hour: " + dateHour[1] + " Temp: " + temp + " Rain: " + humidity + " Cloud: " + cloud + " weather_code: " + weathercode);//+ "ºC");     
                    UIManager.instance.UpdateWeatherUI(temp.ToString()+"ºC", humidity.ToString()+ "%", currentWeather, wind.ToString()+"Km/h", radiation.ToString()+"W/m2", soilTemp.ToString()+"ºC", soilHum.ToString()+"m3/m3");
                    OnChangeWeatherEvent.Invoke(currentWeather, weathercode, wind, radiation); //Listened by VirtualWeather
                }
            }

            i++;
        }
    } 

    private string SearchWeatherStateByCode(int searchCode)
    {
        string weatherDesc = "";
        foreach (KeyValuePair<Code, string> pair in WeatherStates)
        {
            if((int)pair.Key == searchCode)
            {
                //Debug.Log(pair.Key.ToString() + "  -  " + pair.Value.ToString());
                weatherDesc = pair.Value.ToString();
            }
        }
        return weatherDesc;
    }

    public void SearchWeatherAPI(string latitude, string longitude)
    {
        //Debug.Log("Lat: " + latitude + " Long: " + longitude);
        urlOpenMeteo = "https://api.open-meteo.com/v1/forecast?latitude="+latitude+"&longitude="+longitude+"&hourly="+par_temperature+","+par_humidity+","+par_wcode+","+par_cloud+","+par_radiation+","+par_wind+","+par_soil_temperature+","+par_soil_moisture;
        StartCoroutine(GetFromOpenMateoAPI(urlOpenMeteo));
    }
}



