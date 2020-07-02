using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Health") <= 0)
        {
            animator.SetBool("IsDying", true);

        }
    }
}
