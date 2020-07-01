﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;
using static ScreenShaker;

public class PlayerHealth : Health
{
  private int healthBonus = 2;
  private int maxHealth = 10;

  public UISlider healthBar;

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
  }

  private void AddHealth()
  {
    if (health + healthBonus < maxHealth)
    {
      health += healthBonus;
    }
    healthBar.SetValue(health);
    animator.SetInteger("Health", health);
  }
}
