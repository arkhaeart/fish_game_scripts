using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimations : MonoBehaviour
{
    public Animator spriteAnimator;

    private const string FORWARD_ANIM = "forward";
    private const string IDLE_ANIM = "idle";
    private const string TO_RIGHT_ANIM = "toRight";
    private const string TO_LEFT_ANIM = "toLeft";

    public void SetTurnAnimation(float angle)
    {
        Debug.Log($"Setting turn animation to angle: {angle}");
        if (angle == 90)
        {
            spriteAnimator.SetTrigger(TO_LEFT_ANIM);
        }
        else
        {
            spriteAnimator.SetTrigger(TO_RIGHT_ANIM);
        }
    }

    public void SetForwardAnimation()
    {
        Debug.Log("Setting forward animation");

        spriteAnimator.SetTrigger(FORWARD_ANIM);
    }

    public void SetIdleAnimation()
    {
        Debug.Log("Setting idle animation");

        spriteAnimator.SetTrigger(IDLE_ANIM);
    }
}
