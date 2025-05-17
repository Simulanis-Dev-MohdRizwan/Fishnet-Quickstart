
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeatherUIController : MonoBehaviour
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


    private void OnEnable()
    {
        DayBtn.onClick.AddListener(()=>ChangeWeatherRPC.instance.ChangeVisual("day"));
        NightBtn.onClick.AddListener(()=>ChangeWeatherRPC.instance.ChangeVisual("night"));
        RainBtn.onClick.AddListener(()=>ChangeWeatherRPC.instance.ChangeWeatherRain("rain"));
        NoRainBtn.onClick.AddListener(()=>ChangeWeatherRPC.instance.ChangeWeatherRain("norain"));
    }
    void OnDisable()
    {
        DayBtn.onClick.RemoveListener(()=>ChangeWeatherRPC.instance.ChangeVisual("day"));
        NightBtn.onClick.RemoveListener(()=>ChangeWeatherRPC.instance.ChangeVisual("night"));
        RainBtn.onClick.RemoveListener(()=>ChangeWeatherRPC.instance.ChangeWeatherRain("rain"));
        NoRainBtn.onClick.RemoveListener(()=>ChangeWeatherRPC.instance.ChangeWeatherRain("norain"));
    }
}
