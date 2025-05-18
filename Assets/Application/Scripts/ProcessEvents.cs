using UnityEngine;
using UnityEngine.Events;

public class ProcessEvents : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onStart;
    public UnityEvent onComplete;

    [Header("Audio")]
    public AudioClip audioClip;

    [Header("Text")]
    public string processText;
}