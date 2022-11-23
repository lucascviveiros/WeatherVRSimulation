using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMPro.TMP_Dropdown m_Dropdown;
    private List<Region> listRegion = null;
    private TextMeshProUGUI t_Temperatura, t_Humidade, t_Tempo, t_Vento, t_Radiacao, t_TempSolo, t_HumSolo;

    private void Awake()
    {
        //Initializing regions
        listRegion = new List<Region>();
        PopulateRegion();
        //Finding UI Texts in Canvas
        t_Temperatura = GameObject.Find("Values/TextTemperaturaInput").GetComponent<TextMeshProUGUI>();
        t_Humidade = GameObject.Find("Values/TextHumidadeInput").GetComponent<TextMeshProUGUI>();
        t_Tempo = GameObject.Find("Values/TextTempoInput").GetComponent<TextMeshProUGUI>();
        t_Vento = GameObject.Find("Values/TextVentoInput").GetComponent<TextMeshProUGUI>();
        t_Radiacao = GameObject.Find("Values/TextRadiacaoInput").GetComponent<TextMeshProUGUI>();
        t_TempSolo = GameObject.Find("Values/TextTempSoloInput").GetComponent<TextMeshProUGUI>();
        t_HumSolo = GameObject.Find("Values/TextHumSolo").GetComponent<TextMeshProUGUI>();

        //Finding UI Dropdown in Canvas
        m_Dropdown = GameObject.Find("Canvas/PanelRight/Dropdown").GetComponent<TMPro.TMP_Dropdown>(); 
        m_Dropdown.options.Clear();
    } 

    private void PopulateRegion()
    {
        listRegion.Add(new Region("41.80", "-6.77", "Bragança"));
        listRegion.Add(new Region("41.10", "-7.14", "Vila Nova Foz Côa"));
        listRegion.Add(new Region("41.07", "-7.13", "Salgueiro"));
        listRegion.Add(new Region("41.02", "-7.13", "Muxagata"));
        listRegion.Add(new Region("41.01", "-7.01", "Almendra"));
        listRegion.Add(new Region("41.01", "-6.95", "Escalhão"));
    }

    private void Start() 
    {
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
    }

    public void RegionClick(string region)
    {
        //Debug.Log("Region clicked: " + region);
    }

    //Used to search the region clicked by the user in the dropdown list
    private void SearchWeather(string region)
    {
        Region searchRegion = listRegion.Find( x => x.Name==region);
        if(searchRegion == null){}
        else
        {
            string lat = searchRegion.Latitude;
            string lon = searchRegion.Longitude;
            //Calling WebRequestController Instance
            var WetherCall = WebRequestController.instance;
            WetherCall.SearchWeatherAPI(lat, lon);
        }
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMPro.TMP_Dropdown change)
    {
        Debug.Log("Option selected: " + m_Dropdown.options[m_Dropdown.value].text);
        SearchWeather(m_Dropdown.options[m_Dropdown.value].text);
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
}
