using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsController : MonoBehaviour
{
    [Tooltip("The Animator component attached to this GameObject.")]
    public Animator animator;

    [Tooltip("The name of the animation clip to play.")]
    public string animationName;

    [Tooltip("Event invoked when the animation starts playing.")]
    public UnityEvent onAnimationStart;

    [Tooltip("Event invoked when the animation completes playing.")]
    public UnityEvent onAnimationComplete;

    private bool isPlaying = false;

    /// <summary>
    /// Plays the specified animation. Invokes the onAnimationStart event.
    /// </summary>
    public void PlayAnimation()
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

        animator.Play(animationName);
        isPlaying = true;
        onAnimationStart.Invoke();
    }

    void Update()
    {
        if (isPlaying && animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the current animation state is the one we started and if it's finished
            if (stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1)
            {
                isPlaying = false;
                onAnimationComplete.Invoke();
            }
        }
    }
}

