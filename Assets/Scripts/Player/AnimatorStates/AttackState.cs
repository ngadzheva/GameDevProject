using UnityEngine;
using static AudioManager;

public class AttackState : StateMachineBehaviour {
	private Animator animator;
	private MovementController movementController;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.animator = animator;

		MovementController movementController = animator.GetComponent<MovementController>();
		movementController.SetHorizontalMoveDirection(0);

		PlayGunfireSound();
	}
}
