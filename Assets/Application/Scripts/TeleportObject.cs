using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    // Public variable to hold the target GameObject.  Drag and drop this in the inspector.
    public GameObject objectToTeleport;


    public void Teleport(GameObject target, Vector3 destination)
    {
        if (target != null) // Check if the target object is not null
        {
            target.transform.position = destination;
            Debug.Log(target.name + " teleported to " + destination); //Added Debug Log
        }
        else
        {
            Debug.LogError("Teleport target is null!  Please assign a GameObject to the TeleportObject script."); //Error Handling
        }
    }



     public void TeleportObjectToSpecificPosition(Vector3 position)
    {
        if (objectToTeleport != null)
        {
             Teleport(objectToTeleport, position);
        }
        else
        {
             Debug.LogError("objectToTeleport is not assigned in the inspector!");
        }

    }
}