using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AudioManager;
using static ScreenShaker;

public class EnemyHealth : Health
{

  public static event Action<Vector3> OnEnemyDeath;

  private void Start()
  {
    animator = GetComponent<Animator>();
    screenShaker = Camera.main.GetComponent<ScreenShaker>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Bullet_Player"))
    {
      EnemyTakeDamage();
    }
  }

  protected void EnemyTakeDamage()
  {
    base.TakeDamage();
    if (health == 0)
    {
      OnEnemyDeath?.Invoke(transform.position);
    }
  }
}
