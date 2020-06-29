using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;
using static ScreenShaker;

public class PlayerHealth : MonoBehaviour
{

  [SerializeField]
  [Range(1, 5)]
  protected int hp = 5;

  private Animator animator;

  public Healthbar healthBar;
  public ParticleSystem blood;

  void Start()
  {
    animator = GetComponent<Animator>();
    healthBar.SetMaxHealth(hp);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Bullet_Enemy"))
    {
      TakeDamage();
    }
  }

  protected void TakeDamage()
  {
    hp -= 1;
    animator.SetTrigger("TookDamage");
    animator.SetInteger("Health", hp);
    healthBar.SetHealth(hp);
    PlayEffects();

    if (hp <= 0)
    {
      PlayDeathSound();
      ShakeScreenHeavy();
      Destroy(gameObject);
    }
  }

  private void PlayEffects() {
		blood.Stop();
		blood.Play();
		ShakeScreenLight();
	}
}
