using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Mathf;
using static AudioManager;
using static ScreenShaker;

public class Health : MonoBehaviour
{

  [SerializeField]
  [Range(1, 10)]
  protected int health = 10;

  [SerializeField]
  [Range(0.0f, 1.0f)]
  protected float duration = 0.15f;

  [SerializeField]
  [Range(0.0f, 1.0f)]
  protected float magnitude = 0.1f;

  protected Animator animator;
  public ParticleSystem blood;
  protected ScreenShaker screenShaker;

  public void Die()
  {
    PlayDeathSound();
    screenShaker.StartShake(duration, magnitude * 5);

    Destroy(gameObject, 1);
  }

  protected void TakeDamage()
  {
    int damage = 1;
    health = Max(health - damage, 0);

    animator.SetInteger("Health", health);
    animator.SetTrigger("TookDamage");

    PlayEffects();
  }

  private void PlayEffects()
  {
    blood.Stop();
    blood.Play();
    PlayHitSound();
  }

  public void OnDestroy() {
    if (gameObject.name == "Player") {
      GameManager.Instance.StartCoroutine("EndGame");
    }
  }
}
