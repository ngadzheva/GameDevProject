using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePlayerState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDying", true);
        animator.GetComponent<MovementController>().SetHorizontalMoveDirection(0);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Health>().Die();
    }
}
