
using FishNet.Object;
using UnityEngine;
using UnityEngine.Events;


public class PlayerEvents : NetworkBehaviour
{
   
    private NetworkObject networkObject;
    public UnityEvent LocalClientEvents;
    public UnityEvent RemoteClientEvents;
    public GameObject player;
    public GameObject instructorPlayer;
    public Camera playerCam;
    public Camera instructorCam;

    public bool iAmOwner;

    public bool iAmServer;

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
            iAmServer = true;
        }   
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
        {
            iAmOwner = true;
            if (IsServer)
            {
                player.SetActive(false);
                instructorPlayer.SetActive(true);
                Debug.Log(instructorPlayer.name + this);
                Debug.Log("i am server");
            }

            if (base.IsClientOnly)
            {
                WeatherHandler.SetReference(playerCam.gameObject);
                OperationBillBoard.SetReferences(playerCam.gameObject);
                player.SetActive(true);
                Debug.Log("client");
                if (networkObject.IsOwner)
                {
                    LocalClientEvents?.Invoke();
                }
                else
                {
                    RemoteClientEvents?.Invoke();
                }
                Debug.Log(" ia m client only");
            }
        }
 

    }


}
