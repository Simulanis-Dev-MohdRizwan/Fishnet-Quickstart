using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class ProcessTracker : MonoBehaviour
{
    [Header("Process GameObjects")]
    public List<GameObject> processes;

    [Header("TextMeshPro Reference")]
    public TMP_Text processTextDisplay;

    private int _currentProcessIndex = 0;
    private AudioSource _audioSource;
    private AudioClip _currentAudioClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure all process GameObjects are initially inactive, except the first one
        for (int i = 0; i < processes.Count; i++)
        {
            if (processes[i] != null)
            {
                processes[i].SetActive(i == 0);
                ProcessEvents processComponent = processes[i].GetComponent<ProcessEvents>();
                if (processComponent != null)
                {
                    if (i == 0)
                    {
                        processComponent.onStart.Invoke();
                        PlayProcessAudio(processComponent.audioClip);
                        UpdateProcessText(processComponent.processText);
                    }
                }
                else
                {
                    Debug.LogWarning($"Process GameObject at index {i} does not have a Process component.");
                }
            }
            else
            {
                Debug.LogError($"Process GameObject at index {i} is null!");
            }
        }
    }

    public void CompleteCurrentProcess()
    {
        if (_currentProcessIndex >= 0 && _currentProcessIndex < processes.Count)
        {
            GameObject currentProcessObject = processes[_currentProcessIndex];
            ProcessEvents currentProcess = currentProcessObject.GetComponent<ProcessEvents>();

            if (currentProcess != null)
            {
                currentProcess.onComplete.Invoke();
            }

            // Move to the next process if available
            _currentProcessIndex++;
            if (_currentProcessIndex < processes.Count)
            {
                GameObject nextProcessObject = processes[_currentProcessIndex];
                nextProcessObject.SetActive(true);
                ProcessEvents nextProcess = nextProcessObject.GetComponent<ProcessEvents>();
                if (nextProcess != null)
                {
                    nextProcess.onStart.Invoke();
                    PlayProcessAudio(nextProcess.audioClip);
                    UpdateProcessText(nextProcess.processText);
                }
            }
            else
            {
                Debug.Log("All processes completed!");
                // You might want to add an overall completion event here
            }
        }
    }

    private void PlayProcessAudio(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            _currentAudioClip = clip;
        }
        else if (_audioSource != null && clip == null && _currentAudioClip != null && !_audioSource.isPlaying)
        {
            Debug.Log(_currentAudioClip + " is audio completed");
        }
    }

    private void UpdateProcessText(string text)
    {
        if (processTextDisplay != null)
        {
            processTextDisplay.text = text;
        }
    }
}