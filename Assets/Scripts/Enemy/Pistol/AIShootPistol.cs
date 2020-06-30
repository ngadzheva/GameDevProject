using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class AIShootPistol : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Enemy movementController = animator.GetComponent<Enemy>();
		movementController.Shoot();

		PlayGunfireSound();
	}
}
