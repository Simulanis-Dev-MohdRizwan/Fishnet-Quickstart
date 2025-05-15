using UnityEngine;
using System.Net;
using System.Net.Sockets;
using TMPro;
using FishNet.Transporting.Tugboat;


public class GetLocalIp : MonoBehaviour
{
    public TMP_InputField TMP_InputField;
    public Tugboat Tugboat;

    void Start()
    {
        string localIP = GetLocalIPv4();
        Debug.Log("Local IP Address: " + localIP);
    }

    public void SetIpForClient()
    {
        Tugboat.SetClientAddress(TMP_InputField.text);
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
