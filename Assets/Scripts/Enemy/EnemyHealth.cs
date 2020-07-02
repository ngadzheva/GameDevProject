using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AudioManager;
using static ScreenShaker;

public class EnemyHealth : Health
{

  public static event Action<Vector3> OnEnemyDeath;
  public static event Action OnEnemyDeathPoints;
  private bool enemyDeathInvoked = false;

  private void Start()
  {
    animator = GetComponent<Animator>();
    screenShaker = Camera.main.GetComponent<ScreenShaker>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!animator.GetBool("IsDying") && collision.CompareTag("Bullet_Player"))
    {
      EnemyTakeDamage();
    }
  }

  protected void EnemyTakeDamage()
  {
    base.TakeDamage();
    if (health <= 0 && !enemyDeathInvoked)
    {
      OnEnemyDeath?.Invoke(transform.position);
      OnEnemyDeathPoints?.Invoke();
      enemyDeathInvoked = true;
    }
  }
}
