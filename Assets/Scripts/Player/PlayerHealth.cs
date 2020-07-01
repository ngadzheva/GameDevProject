using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AudioManager;
using static ScreenShaker;

public class PlayerHealth : Health
{
  private int healthBonus = 2;
  private int maxHealth = 10;

  public UISlider healthBar;

  public static event Action OnPlayerDeath;

  void Start()
  {
    healthBar.SetMaxValue(health);
    animator = GetComponent<Animator>();
    screenShaker = Camera.main.GetComponent<ScreenShaker>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Bullet_Enemy"))
    {
      PlayerTakeDamage();
    }
    else if (collision.CompareTag("Blood"))
    {
      AddHealth();
      Destroy(collision.gameObject);
    }
  }

  protected void PlayerTakeDamage()
  {
    base.TakeDamage();
    healthBar.SetValue(health);

    if (health <= 0)
    {
      OnPlayerDeath?.Invoke();
    }
  }

  private void AddHealth()
  {
    if (health < maxHealth)
    {
      int newHealth = health + healthBonus;

      if (newHealth <= maxHealth)
      {
        health = newHealth;
      }
      else
      {
        health += (newHealth - maxHealth);
      }
    }

    healthBar.SetValue(health);
    animator.SetInteger("Health", health);
  }
}
