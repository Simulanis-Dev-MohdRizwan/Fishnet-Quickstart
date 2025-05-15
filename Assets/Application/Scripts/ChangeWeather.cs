using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;
using UnityEngine;

public class ChangeWeather : NetworkBehaviour
{
    [ServerRpc]
    public void ChangeDayTime(NetworkConnection networkConnection = null ,string dayTime)
    {
        SetClientDayTime(dayTime);
    }
    
    [ObserversRpc]
    void SetClientDayTime(string day) {
        Debug.Log(" Its : " + day);
    }
}
