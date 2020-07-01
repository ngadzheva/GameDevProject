using UnityEngine;
using static AudioManager;

public class DieState : StateMachineBehaviour
{
  MovementController gameObjectMovement;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.SetBool("IsDying", true);

    gameObjectMovement = animator.GetComponent<MovementController>();
    if (gameObjectMovement) gameObjectMovement.SetHorizontalMoveDirection(0);

    animator.GetComponent<Health>().Die();
  }
}
