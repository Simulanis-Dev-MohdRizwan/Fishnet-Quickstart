using FishNet.Object;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeWeather : NetworkBehaviour
{
    public Button DayBtn;
    public Button NightBtn;
    public Button RainBtn;
    public Button NoRainBtn;


    [Header("Weather/Day Events")]
    public UnityEvent OnDaySelected;
    public UnityEvent OnNightSelected;
    public UnityEvent OnRainSelected;
    public UnityEvent OnRainOffSelected;

    [ServerRpc]
    public void ChangeDayTime(string dayTime, NetworkConnection networkConnection = null)
    {
        SetClientDayTime(dayTime);
    }

     [ServerRpc]
    public void ChangeRainWeather(string rain, NetworkConnection networkConnection = null)
    {
        SetClientWeather(rain);
    }

    [ObserversRpc]
    void SetClientWeather(string rain)
    {
        WeatherHandler.weather.CustomSetRainy(rain);
    }

    [ObserversRpc]
    void SetClientDayTime(string day)
    {
        if (day.ToLower().Equals("day"))
        {
            OnDaySelected?.Invoke();
        } else if(day.ToLower().Equals("night"))
        {
            OnNightSelected?.Invoke();
        } else
        {
            Debug.Log("Write day or night only");
        }

        WeatherHandler.weather.CustomSetDayTime(day);
        Debug.Log(" Its : " + day);
    }
    private void OnEnable()
    {
        DayBtn.onClick.AddListener(() => ChangeDayTime("Day"));
        NightBtn.onClick.AddListener(() => ChangeDayTime("Night"));
        RainBtn.onClick.AddListener(() => SetClientWeather("rain"));
        NoRainBtn.onClick.AddListener(() => SetClientWeather("norain"));
    }
    void OnDisable()
    {
        DayBtn.onClick.RemoveListener(() => ChangeDayTime("Day"));
        NightBtn.onClick.RemoveListener(() => ChangeDayTime("Night"));
        RainBtn.onClick.RemoveListener(() => SetClientWeather("rain"));
        NoRainBtn.onClick.RemoveListener(() => SetClientWeather("norain"));
    }




}
