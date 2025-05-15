using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using FishNet.Transporting.Tugboat;
using UnityEngine;

public class SetupHost : MonoBehaviour
{
    public delegate void HostEvents(string address);
    public static HostEvents SetupHostAdressOnEnable;
    [SerializeField]
    private Tugboat tugboat;

    private void OnEnable()
    {
        if (tugboat == null)
        {
            tugboat = FindObjectOfType<Tugboat>();
        }
        tugboat.SetClientAddress(GetLocalIPv4());
    }
    
     public string GetLocalIPv4()
    {
        string localIP = string.Empty;
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        if (string.IsNullOrEmpty(localIP))
        {
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
        return localIP;
    }

}
