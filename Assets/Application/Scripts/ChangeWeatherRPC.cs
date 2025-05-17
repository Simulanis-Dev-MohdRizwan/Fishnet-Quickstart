using FishNet.Object;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MonoFN.Cecil.Cil;
using JetBrains.Annotations;

public class ChangeWeatherRPC : NetworkBehaviour
{
    public static ChangeWeatherRPC instance;
    public WeatherHandler weatherHandler;

    private void Start() {
        if (weatherHandler == null)
        {
          weatherHandler = FindObjectOfType<WeatherHandler>();
        }
    }

   [ServerRpc(RequireOwnership = false)]
    public void ChangeDayTime(string dayTime)
    {
        SetClientDayTime(dayTime);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeRainWeather(string rain)
    {
        SetClientWeather(rain);
    }

    [ObserversRpc(BufferLast = true, RunLocally = true, IncludeOwner = true)]
    void SetClientWeather(string rain)
    {
        
        Debug.Log(" Its : " + rain);
        weatherHandler.CustomSetRainy(rain);
    }

    [ObserversRpc(BufferLast = true, RunLocally = true, IncludeOwner = true)]
    void SetClientDayTime(string day)
    {
        Debug.Log(" Its : " + day);
        weatherHandler.CustomSetDayTime(day);
    }

    public void ChangeVisual(string day)
    {
        ChangeDayTime(day);
    }
       public void ChangeWeatherRain(string rain)
    {
        ChangeRainWeather(rain);
    }
    private void OnEnable()
    {
        instance = this;
    }




}
