using UnityEngine;
using DigitalRuby.RainMaker;

public class VirtualWeather : Singleton<VirtualWeather>
{
    [SerializeField] private Light sun;
    [SerializeField] private RainScript rain;
    [SerializeField] private Material m_tree;

    private void Awake() 
    {
        rain = FindObjectOfType<RainScript>();
        sun = GameObject.Find("Scene/Directional Light").GetComponent<Light>();
        sun.intensity = 1.2f;
        rain.RainIntensity = 0.0f;
    }

    public void OnChangeWeatherReceiver(string currentWeather, int code, double wind, double radiation) {
     
        //Debug.Log("weaher: " + currentWeather + " code " + code + " Wind: " + wind);
        //Wind control 
        m_tree.SetFloat("_MotionSpeed", (float)wind / 3.6f);

        //Sun Intensity based on radiation paremeter
        sun.intensity = (float)radiation * 0.00444f;
        //Debug.Log("sun: " + sun.intensity.ToString());

        if (code == 0) //ceu limpo
        {
            rain.RainIntensity = 0f;
        }
        else if (code == 1 || code == 2 || code == 3) //Parcialmente Nublado
        {
            rain.RainIntensity = 0f;
        }
        else if(code == 45 || code == 48 ) //nevoeiro e geada
        {
            rain.RainIntensity = 0f;
        }
        else if(code == 51 || code == 53 || code == 55) //Garoa
        {
            rain.RainIntensity = 0.015f;
        }
        else if(code == 56 || code == 57) //Garoa congelante
        {
            rain.RainIntensity = 0.12f;
        }
        else if(code == 61 || code == 63 || code == 65) //Chuva
        {
            rain.RainIntensity = 0.12f;
        }
        else if(code == 66 || code == 67) //Chuva congelante
        {
            rain.RainIntensity = 0.12f;
        }
        else if(code == 71 || code == 73 || code == 75) //Queda de neve
        {
            rain.RainIntensity = 0f;
        }
        else if(code == 77) //graos de neve
        {
            rain.RainIntensity = 0f;
        }
        else if(code == 80 || code == 81 || code == 82) //Pancadas de chuva 
        {
            rain.RainIntensity = 1f;
        }
        else if(code == 85|| code == 86) //aguaceiros de neve
        {
            rain.RainIntensity = 1f;
        }
        else if (code == 95 ) //trovoada
        {
            rain.RainIntensity = 1f;
        }
        else if (code == 96|| code == 99) //trovoada com granizo
        {
            rain.RainIntensity = 1f;
        }
    }

    public void SunSimulationSlider(float sliderValue)
    {
        sun.intensity = sliderValue * 2;
    }

    public void RainSimulationSlider(float sliderValue)
    {
        rain.RainIntensity = sliderValue;
    }

    public void WindSimulationSlider(float sliderValue)
    {
        m_tree.SetFloat("_MotionSpeed", sliderValue * 3);
        //m_tree.SetFloat("_MotionRange", sliderValue * 2);
    }
}
