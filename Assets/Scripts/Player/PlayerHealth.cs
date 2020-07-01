﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;
using static ScreenShaker;

public class PlayerHealth : MonoBehaviour
{

  [SerializeField]
  [Range(1, 10)]
  protected int hp = 10;

  [SerializeField]
  [Range(0.0f, 1.0f)]
  private float duration = 0.15f;

  [SerializeField]
  [Range(0.0f, 1.0f)]
  private float magnitude = 0.1f;

  private int healthBonus = 2;
  private int maxHealth = 10;

  private Animator animator;

  public UISlider healthBar;
  public ParticleSystem blood;
  public ScreenShaker screenShaker;

  void Start()
  {
    animator = GetComponent<Animator>();
    healthBar.SetMaxValue(hp);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Bullet_Enemy"))
    {
      TakeDamage();
    }
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Blood"))
    {
      AddHealth();
      Destroy(other.gameObject);
    }
  }

  protected void TakeDamage()
  {
    hp -= 1;
    animator.SetTrigger("TookDamage");
    animator.SetInteger("Health", hp);
    healthBar.SetValue(hp);
    PlayEffects();

    if (hp <= 0)
    {
      PlayDeathSound();
      screenShaker.StartShake(duration, magnitude);
      Destroy(gameObject);
    }
  }

  private void AddHealth()
  {
    if (hp + healthBonus < maxHealth)
    {
      hp += healthBonus;
    }
    healthBar.SetValue(hp);
  }

  private void PlayEffects()
  {
    blood.Stop();
    blood.Play();
    PlayHitSound();
    screenShaker.StartShake(duration, magnitude);
  }
}
