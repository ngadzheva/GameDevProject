using UnityEngine;

public class AttackState : StateMachineBehaviour {
	private Animator animator;
	private MovementController movementController;
	private GameObject fireball;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		this.animator = animator;

		MovementController movementController = animator.GetComponent<MovementController>();
		movementController.SetHorizontalMoveDirection(0);

		fireball = animator.transform.GetChild(0).gameObject;
		fireball.SetActive(true);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		fireball.SetActive(false);
	}
}
