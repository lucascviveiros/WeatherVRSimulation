using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMPro.TMP_Dropdown m_Dropdown;
    [SerializeField] private Slider sl_sun;
    [SerializeField] private Slider sl_rain;
    [SerializeField] private Slider sl_wind;
    private List<Region> listRegion = null;
    private TextMeshProUGUI t_Temperatura, t_Humidade, t_Tempo, t_Vento, t_Radiacao, t_TempSolo, t_HumSolo, t_SunSlider, t_RainSlider;

    private void Start()
    {
        //Initializing regions
        listRegion = new List<Region>();
        PopulateRegion();

        //Finding UI Texts in Canvas
        t_Temperatura = GameObject.Find("UI/Output/TextTemperatureValues").GetComponent<TextMeshProUGUI>();
        t_Humidade = GameObject.Find("UI/Output/TextHumidityValues").GetComponent<TextMeshProUGUI>();
        t_Tempo = GameObject.Find("UI/Output/TextWeatherConditionValues").GetComponent<TextMeshProUGUI>();
        t_Vento = GameObject.Find("UI/Output/TextWindValues").GetComponent<TextMeshProUGUI>();
        t_Radiacao = GameObject.Find("UI/Output/TextRadiationValues").GetComponent<TextMeshProUGUI>();
        t_TempSolo = GameObject.Find("UI/Output/TextSoilTemperatureValues").GetComponent<TextMeshProUGUI>();
        t_HumSolo = GameObject.Find("UI/Output/TextSoilHumidityValues").GetComponent<TextMeshProUGUI>();
        t_SunSlider = GameObject.Find("UI/Output/TextSunSliderValues").GetComponent<TextMeshProUGUI>();
        t_RainSlider = GameObject.Find("UI/Output/TextRainSliderValues").GetComponent<TextMeshProUGUI>();
        t_SunSlider.text = "";
        t_RainSlider.text = "";

        //Finding UI Dropdown in Canvas
        m_Dropdown = GameObject.Find("UI/DropdownRegion").GetComponent<TMPro.TMP_Dropdown>();
        m_Dropdown.options.Clear();

        //Finding UI Slider in Canvas
        sl_sun = GameObject.Find("UI/SliderSun").GetComponent<Slider>();
        sl_rain = GameObject.Find("UI/SliderRain").GetComponent<Slider>();
        sl_wind = GameObject.Find("UI/SliderWind").GetComponent<Slider>();

        //Return Sliders to zero
        ReturnSliders();

        //Adding regions in Dropdown UI
        foreach (Region option in listRegion)
        {
            m_Dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(option.Name));
        }
        //Creating Dropdown select event
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
        //Creating Slider movement event
        sl_sun.onValueChanged.AddListener(delegate
        {
            ValueChangedSliderSun();
        });
        sl_rain.onValueChanged.AddListener(delegate {

            ValueChangedSliderRain();
        });
        
        sl_wind.onValueChanged.AddListener(delegate {

            ValueChangedSliderWind();
        }); 
    }

    private void PopulateRegion()
    {
        listRegion.Add(new Region("-23.53", "-46.62", "São Paulo - BR"));

        listRegion.Add(new Region("52.37", "4.89", "Amsterdam - NL"));

        listRegion.Add(new Region("48.86", "2.34", "Paris - FR"));

        listRegion.Add(new Region("38.72", "-9.13", "Lisbon - PT"));

        //listRegion.Add(new Region("41.80", "-6.77", "Bragança"));
        //listRegion.Add(new Region("41.10", "-7.14", "Vila Nova Foz Côa"));
        //listRegion.Add(new Region("41.07", "-7.13", "Salgueiro"));
        //listRegion.Add(new Region("41.02", "-7.13", "Muxagata"));
        //listRegion.Add(new Region("41.01", "-7.01", "Almendra"));
        //listRegion.Add(new Region("41.01", "-6.95", "Escalhão"));
    }

    public void RegionClick(string region)
    {
        //Debug.Log("Region clicked: " + region);
    }

    //Search the region clicked by the user in the dropdown list
    private void SearchWeather(string region)
    {
        Region searchRegion = listRegion.Find(x => x.Name == region);
        if (searchRegion == null) { }
        else
        {
            string lat = searchRegion.Latitude;
            string lon = searchRegion.Longitude;
            //Calling WebRequestController Instance
            var WeatherCall = WebRequestController.instance;
            WeatherCall.SearchWeatherAPI(lat, lon);
        }
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMPro.TMP_Dropdown change)
    {
        //Debug.Log("Option selected: " + m_Dropdown.options[m_Dropdown.value].text);
        SearchWeather(m_Dropdown.options[m_Dropdown.value].text);
    }

    public void ReturnSliders()
    {
        sl_sun.value = 0f;
        sl_rain.value = 0f;
        sl_wind.value = 0f;
    }

    //Used to update weather parameters in the UI Text in the Canvas from WebRequestController
    public void UpdateWeatherUI(string temp, string hum, string tempo, string vento, string radiacao, string tempSolo, string humSolo)
    {
        t_Temperatura.text = temp;
        t_Humidade.text = hum;
        t_Tempo.text = tempo;
        t_Vento.text = vento;
        t_Radiacao.text = radiacao;
        t_TempSolo.text = tempSolo;
        t_HumSolo.text = humSolo;
    }

    private void ValueChangedSliderSun()
    {
        float sliderSunValue = sl_sun.value;
        //Debug.Log("ValueChangedSliderSun: " + sliderSunValue);
        if (sliderSunValue >= 0.3f)
            VirtualWeather.instance.SunSimulationSlider(sliderSunValue);
    }

    private void ValueChangedSliderRain()
    {
        float sliderRainValue = sl_rain.value;
        //Debug.Log("ValueChangedSliderSun: " + sliderRainValue);
        VirtualWeather.instance.RainSimulationSlider(sliderRainValue);
    }
    
    private void ValueChangedSliderWind()
    {
        float sliderWindValue = sl_wind.value * 3;
        //Debug.Log("ValueChangedSliderSun: " + sliderWindValue);
        if (sliderWindValue >= 1.5f)
            VirtualWeather.instance.WindSimulationSlider(sliderWindValue);
    }
}