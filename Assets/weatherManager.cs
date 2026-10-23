using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class weatherManager : MonoBehaviour
{
    enum Weather
    {
        sunny, rainy, foggy
    }
    private Weather weather;
    private bool outside = false;

    public void setWeather(int day)
    {
        switch (day)
        {
            case 1:
                weather = Weather.sunny; break;
            case 2:
                weather = Weather.rainy; break;
            case 3:
                weather = Weather.foggy; break;
        }
    }

    public void activateWeather()
    {
        switch(weather)
        {
            case Weather.sunny:
                if (outside) { }
                else { }
                break;
            case Weather.rainy:
                if (outside) { FindFirstObjectByType<rainScript>().setRain(false); outside = false; }
                else { FindFirstObjectByType<rainScript>().setRain(true); outside = true; }
                break;
            case Weather.foggy:
                if (outside) { }
                else { }
                break;
        }
    }
}
