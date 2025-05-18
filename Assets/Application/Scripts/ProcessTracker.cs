using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using FishNet.Object;

public class ProcessTracker : NetworkBehaviour
{
    [Tooltip("List of GameObjects representing individual processes. Order defines the sequence.")]
    public List<GameObject> processObjects;

    public int _currentProcessIndex = 0;
    private List<Process> _processes = new List<Process>();

    [System.Serializable]
    public class Process
    {
        public GameObject gameObject;
        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onComplete = new UnityEvent();
    }

    void Awake()
    {
        InitializeProcesses();
    }

    void Start()
    {
        if (_processes.Count > 0)
        {
            ActivateCurrentProcess();
        }
        else
        {
            Debug.LogWarning("No process GameObjects assigned in the Process Tracker.");
        }
    }

    void InitializeProcesses()
    {
        foreach (GameObject processObject in processObjects)
        {
            Process process = new Process();
            process.gameObject = processObject;

            // Try to find OnStart and OnComplete events on the GameObject
            ProcessEvents processEvents = processObject.GetComponent<ProcessEvents>();
            if (processEvents != null)
            {
                process.onStart = processEvents.onStart;
                process.onComplete = processEvents.onComplete;
            }
            else
            {
                Debug.LogWarning($"Process GameObject '{processObject.name}' does not have a ProcessEvents component. Using default UnityEvents.");
            }

            _processes.Add(process);
            // Ensure all process GameObjects are initially inactive
            processObject.SetActive(false);
        }
    }

    void ActivateCurrentProcess()
    {
        if (_currentProcessIndex >= 0 && _currentProcessIndex < _processes.Count)
        {
            _processes[_currentProcessIndex].gameObject.SetActive(true);
            _processes[_currentProcessIndex].onStart.Invoke();
            Debug.Log($"Process '{_processes[_currentProcessIndex].gameObject.name}' started.");
        }
        else
        {
            Debug.Log("All processes completed.");
        }
    }

    public void CompleteCurrentProcess()
    {
        if (_currentProcessIndex >= 0 && _currentProcessIndex < _processes.Count)
        {
            _processes[_currentProcessIndex].onComplete.Invoke();
            _processes[_currentProcessIndex].gameObject.SetActive(false);
            processObjects[_currentProcessIndex].GetComponent<ProcessEvents>().completed = true;

            Debug.Log($"Process '{_processes[_currentProcessIndex].gameObject.name}' completed.");
            MoveToNextProcess();
        }
        else
        {
            Debug.LogWarning("No active process to complete.");
        }
    }

    [ContextMenu("Move to next")]
    public void MoveToNextProcess()
    {
        _currentProcessIndex++;
        //ActivateCurrentProcess();

        SyncVarOnServer(_currentProcessIndex);
    }

    public void MoveToPreviousProcess()
    {
        _currentProcessIndex--;
        if (_currentProcessIndex < 0)
        {
            _currentProcessIndex = 0;
            Debug.Log("Already at the first process.");
        }
        ActivateCurrentProcess();
    }

    public int GetCurrentProcessIndex()
    {
        return _currentProcessIndex;
    }

    public GameObject GetCurrentProcessObject()
    {
        if (_currentProcessIndex >= 0 && _currentProcessIndex < _processes.Count)
        {
            return _processes[_currentProcessIndex].gameObject;
        }
        return null;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncVarOnServer(int currentStep)
    {
        SyncVarOnObserver(currentStep);
        Debug.Log("current step on Server is: " + currentStep);
    }

    [ObserversRpc(BufferLast = true, RunLocally = true, IncludeOwner = true)]
    public void SyncVarOnObserver(int currentStep)
    {
        Debug.Log("current step on observer is: " + currentStep);
        if (_currentProcessIndex != currentStep)
        {
            _currentProcessIndex = currentStep;
        }
        ActivateCurrentProcess();
    }

}