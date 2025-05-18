using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsController : MonoBehaviour
{
    [Tooltip("The Animator component attached to this GameObject.")]
    public Animator animator;

    [Tooltip("Event invoked when the animation starts playing.")]
    public UnityEvent onAnimationStart;

    [Tooltip("Event invoked when the animation completes playing.")]
    public UnityEvent onAnimationComplete;

    private bool isPlaying = false;
    private bool isPaused = false;
    private string currentAnimationName; 

    public void PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
            return;
        }

        if (string.IsNullOrEmpty(animationName))
        {
            Debug.LogError("Animation Name is not specified.");
            return;
        }
        if (isPlaying && currentAnimationName != animationName)
        {
             animator.Play(animationName, 0, 0f); //restart animation if a different animation is called.
        }
        else
        {
            animator.Play(animationName);
        }

        currentAnimationName = animationName; 
        isPlaying = true;
        isPaused = false; 
        onAnimationStart.Invoke();
    }

    public void PauseAnimation()
    {
        if (isPlaying && !isPaused && animator != null)
        {
            animator.speed = 0f; 
            isPaused = true;
        }
    }


    public void ResumeAnimation()
    {
        if (isPlaying && isPaused && animator != null)
        {
            animator.speed = 1f;
            isPaused = false;
        }
    }

    void Update()
    {
        if (isPlaying && !isPaused && animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the current animation state is the one we started and if it's finished
            if (stateInfo.IsName(currentAnimationName) && stateInfo.normalizedTime >= 1)
            {
                isPlaying = false;
                onAnimationComplete.Invoke();
            }
        }
    }
}
