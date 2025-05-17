using System;
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
    public static System.Action<GameObject> SetReference;

    public Tenkoku.Core.TenkokuModule tenkokuObject;

    private void Start()
    {
        SetReference += SetMainCamera;
        tenkokuObject = (Tenkoku.Core.TenkokuModule)FindObjectOfType(typeof(Tenkoku.Core.TenkokuModule));
    }
    public void CustomSetRainy(string rain)
    {
        if (rain.ToLower().Equals("rain"))
        {
            tenkokuObject.weather_RainAmt = 1f;
            tenkokuObject.weather_RainAmt = 0.4f;
        }
        else if (rain.ToLower().Equals("norain"))
        {
            tenkokuObject.weather_RainAmt = 0f;
            tenkokuObject.weather_RainAmt = 0.1f;
        }
    }

    public void CustomSetDayTime(string day)
    {
        if (day.ToLower().Equals("day"))
        {
            tenkokuObject.setHour = 10;
        }
        else if (day.ToLower().Equals("night"))
        {
            tenkokuObject.setHour = 19;
        }
    }
     void SetMainCamera(GameObject cam)
    {
        tenkokuObject.enabled = true;
        tenkokuObject.mainCamera = cam.transform;
    }

    
}
