using UnityEngine;
using static AudioManager;

public class DieState : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Health>().Die();
    }
}
