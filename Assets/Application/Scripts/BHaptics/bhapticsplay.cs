using Bhaptics.SDK2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bhapticsplay : MonoBehaviour
{

    public static Action playRightDevice, PlayLeftDevie;

    public bool PlayLeftContinuosly, PlayRightContinuously;

    private void Update()
    {
        if (PlayLeftContinuosly)
        {
            PlayLeftHand();
        }

        if (PlayRightContinuously)
        {
            PlayRightHand();
        }
    }


    private void OnEnable()
    {
        playRightDevice += PlayRightHand;
        PlayLeftDevie += PlayLeftHand;
    }

    private void OnDisable()
    {
        playRightDevice -= PlayRightHand;
        PlayLeftDevie -= PlayLeftHand;
    }

    [ContextMenu("Play Left")]
    public void PlayLeftHand()
    {
        bhaptics_library.play("vibrate");
    }

    [ContextMenu("Play Right")]
    public void PlayRightHand()
    {
        bhaptics_library.play("vibrate2");
    }

}