using Bhaptics.SDK2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bhapticsplay : MonoBehaviour
{

    public static Action playRightDevice, PlayLeftDevie;

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