using UnityEngine;
using System;
using System.Collections;
using static Controls;
using static UnityEngine.Mathf;

public class StateMachineUtil : MonoBehaviour {

	private static StateMachineUtil instance;

	private void Start() {
		instance = this;
	}

	public static void DoHorizontalMove(Animator animator, MovementController movementController) {
		Debug.Log("here");
		float horizontalMoveDirection = Input.GetAxisRaw(HorizontalMovementAxis);
		movementController.SetHorizontalMoveDirection(horizontalMoveDirection);
		animator.SetFloat("HorizontalMovement", Abs(horizontalMoveDirection));
	}

	public static void DoVerticalMove(Animator animator, MovementController movementController) {
		float verticalMoveDirection = Input.GetAxisRaw(VerticalMovementAxis);
		movementController.SetVerticalMoveDirection(verticalMoveDirection);
		animator.SetFloat("VerticalMovement", Abs(verticalMoveDirection));
	}

	public static void DoWithDelay(float delayInSeconds, Action action) {
		instance.DoWithDelayUtil(delayInSeconds, action);
	}

	private void DoWithDelayUtil(float delayInSeconds, Action action) {
		StartCoroutine(DoWithDelayCoroutine(delayInSeconds, action));
	}

	private IEnumerator DoWithDelayCoroutine(float delayInSeconds, Action action) {
		yield return new WaitForSeconds(delayInSeconds);
		action();
	}
}
