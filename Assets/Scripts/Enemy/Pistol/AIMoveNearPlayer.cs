using UnityEngine;
using System;

public class AIMoveNearPlayer : StateMachineBehaviour
{
  [SerializeField]
  private float wantedDistanceToPlayer = 5;

  private Transform player;
  private Enemy enemyController;
  private float lastTimeShoot = 0;

  public static Func<float> TimeModif;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    GameObject playerGameObject = GameObject.FindWithTag("Player");
    if (playerGameObject == null)
    {
      Debug.LogError("No GameObject with the \"Player\" tag found");
    }
    else
    {
      player = playerGameObject.transform;
    }

    enemyController = animator.GetComponent<Enemy>();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (player == null)
    {
      Debug.LogError("No player found");
    }
    else
    {
      Vector3 vectorToPlayer = player.position - animator.transform.position;
      float distanceToPlayer = vectorToPlayer.magnitude;

      if (distanceToPlayer > wantedDistanceToPlayer)
      {
        enemyController.MoveToPosition(player.position);
      }

      float timeModif = 1f;
      float? newTineModif = TimeModif?.Invoke();
      if (newTineModif.HasValue)
      {
        timeModif = newTineModif.Value;
      }

      float currTime = Time.time;
      if (currTime - lastTimeShoot > enemyController.shootRecharge / timeModif)
      {
        animator.SetTrigger("ShouldAttack");
        lastTimeShoot = currTime;
      }
    }
  }
}
