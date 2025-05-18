using UnityEngine;
using UnityEngine.Events;

public class ProcessEvents : MonoBehaviour
{
    public bool completed;
    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onComplete = new UnityEvent();
}