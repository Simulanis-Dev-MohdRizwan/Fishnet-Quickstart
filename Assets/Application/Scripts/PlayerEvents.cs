using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Object;
using FishNetQuckstart.Advanced;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerEvents : NetworkBehaviour
{
    private NetworkObject networkObject;
    public UnityEvent LocalClientEvents;
    public UnityEvent RemoteClientEvents;
    public GameObject player;
    public GameObject instructorPlayer;

    void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }
    private void OnEnable()
    {
        if (instructorPlayer == null)
        {
            instructorPlayer = transform.Find("Instructor").gameObject;
        }
        if (player == null)
        {
            player = transform.Find("VRPlayer").gameObject;
        }

    }


    public override void OnStartServer()
    {
        base.OnStartServer();
        if (base.IsServer)
        {
           
                 player.SetActive(false);
                instructorPlayer.SetActive(true);
                Debug.Log("i am server");

         
        }   
          
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        if (base.IsClientOnly)
        {
            player.SetActive(true);
            Debug.Log("client");
            if (networkObject.IsOwner)
            {
                Debug.Log(this + " local");
                LocalClientEvents?.Invoke();
            }
            else
            {
                Debug.Log(this + "remote");
                RemoteClientEvents?.Invoke();
            }
            Debug.Log(" ia m client only");
        }
        if (base.IsServer)
        {

            player.SetActive(false);
            instructorPlayer.SetActive(true);
            instructorPlayer.name = instructorPlayer.name + "Server";
            Debug.Log(instructorPlayer.name + this);
        }
    }


}
