using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public string isInvulnerable = "isInvulnerable";
    public bool isInvulnerableStatus = false;
    public string isInteracting = "isInteracting";
    public bool isInteractingStatus = false;

    public string isFiringSpell = "isFiringSpell";
    public bool isfiringSpellStatus = false;

    public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
    public bool isRotatingWithRootMotionStatus = false;

    public string canRotate = "canRotate";
    public bool canRotateStatus = true;




    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInvulnerable, isInvulnerableStatus);
        animator.SetBool(isInteracting, isInteractingStatus);
        animator.SetBool(isFiringSpell, isfiringSpellStatus);
        animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
        animator.SetBool(canRotate, canRotateStatus);

    }




}
