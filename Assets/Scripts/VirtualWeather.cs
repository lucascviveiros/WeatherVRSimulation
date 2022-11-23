using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualWeather : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject rain;

    private void Awake() 
    {
        sun = GameObject.Find("Scene/Directional Light").GetComponent<Light>();
        sun.intensity = 2f;    
    }

    public void OnChangeWeatherReceiver(string currentHour, string currentWeather, int code) {
     
        Debug.Log("example " + currentHour + " " + currentWeather + " code " + code);

        if(code == 0) //ceu limpo
        {

        }
        else if (code == 1 || code == 2 || code == 3) //Parcialmente Nublado
        {

        }
        else if(code == 45 || code == 48 ) //nevoeiro e geada
        {

        }
        else if(code == 51 || code == 53 || code == 55) //Garoa
        {

        }
        else if(code == 56 || code == 57) //Garoa congelante
        {

        }
        else if(code == 61 || code == 63 || code == 65) //Chuva
        {

        }
        else if(code == 66 || code == 67) //Chuva congelante
        {

        }
        else if(code == 71 || code == 73 || code == 75) //Queda de neve
        {

        }
        else if(code == 77) //graos de neve
        {

        }
        else if(code == 80 || code == 81 || code == 82) //Pancadas de chuva 
        {

        }
        else if(code == 85|| code == 86) //aguaceiros de neve
        {

        }
        else if (code == 95 ) //trovoada
        {

        }
        else if (code == 96|| code == 99) //trovoada com granizo
        {

        }
    }
}
