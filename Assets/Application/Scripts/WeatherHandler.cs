using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
    public static WeatherHandler weather;
    public Tenkoku.Core.TenkokuModule tenkokuObject;

    private void Start() {
        tenkokuObject = (Tenkoku.Core.TenkokuModule) FindObjectOfType(typeof(Tenkoku.Core.TenkokuModule));
    }
    public void CustomSetRainy(string rain)
    {
        if (rain.ToLower().Equals("rain"))
        {
            tenkokuObject.weather_RainAmt = 1f;
            tenkokuObject.weather_RainAmt = 0.4f;
        }
        else if(rain.ToLower().Equals("norain"))
        {
            tenkokuObject.weather_RainAmt = 0f;
            tenkokuObject.weather_RainAmt = 0.1f;
        }
    }

    public void CustomSetDayTime(string day)
    {
        if (day.ToLower().Equals("day"))
        {
            tenkokuObject.dayValue = 10f;
        }
        else if (day.ToLower().Equals("night"))
        {
             tenkokuObject.dayValue = 18.5f;
        }
     }
    
}
