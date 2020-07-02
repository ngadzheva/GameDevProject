using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Controlls;
using static AudioManager;

public class SlowTimeController : MonoBehaviour
{
  private Vector3 lastNonZeroInputDirection = Vector3.right;

  private bool slowTime = false;
  private float slowTimeCharge = 100f;
  private float slowTimeChargeUseRate = 10f;
  private float slowTimeChargeReplenishRate = 2f;

  [SerializeField]
  [Range(0, 50)]
  private float slowTimeBonus = 40f;

  [SerializeField]
  [Range(0, 100)]
  private float maxSlowTimeCharge = 100f;

  public UISlider power;

  void Update()
  {

    if (Input.GetKeyDown(slowTimeKey)) { SlowTime(); }

    SlowTimeChargeHandle();
  }

  private void SlowTime()
  {
    slowTime = !slowTime;
    if (slowTime)
    {
      Bullet.TimeModif += SlowTimeModif;
      Enemy.TimeModif += SlowTimeModif;
      PlayerGun.TimeModif += SlowTimeModif;
      AIMoveNearPlayer.TimeModif += SlowTimeModif;
      EnemySpawner.TimeModif += SlowTimeModif;
    }
    else
    {
      Bullet.TimeModif -= SlowTimeModif;
      Enemy.TimeModif -= SlowTimeModif;
      PlayerGun.TimeModif -= SlowTimeModif;
      AIMoveNearPlayer.TimeModif -= SlowTimeModif;
      EnemySpawner.TimeModif -= SlowTimeModif;
    }
  }

  private float SlowTimeModif()
  {
    return 0.25f;
  }

  private void SlowTimeChargeHandle()
  {
    if (slowTime && slowTimeCharge > 0)
    {
      slowTimeCharge -= slowTimeChargeUseRate * Time.deltaTime;
      power.SetValue((int)slowTimeCharge);
    }
    else if (slowTime && slowTimeCharge <= 0)
    {
      SlowTime();
      slowTimeCharge = 0;
      power.SetValue((int)slowTimeCharge);
    }
    else if (!slowTime && slowTimeCharge < 100)
    {
      slowTimeCharge += slowTimeChargeReplenishRate * Time.deltaTime;
      power.SetValue((int)slowTimeCharge);
    }
    else if (!slowTime && slowTimeCharge > 100)
    {
      slowTimeCharge = 100;
      power.SetValue((int)slowTimeCharge);
    }
  }

  private void AddSlowTimeCharge()
  {
    if (slowTimeCharge < maxSlowTimeCharge)
    {
      float newSlowTimeCharge = slowTimeCharge + slowTimeBonus;

      if (newSlowTimeCharge <= maxSlowTimeCharge)
      {
        slowTimeCharge = newSlowTimeCharge;
      }
      else
      {
        slowTimeCharge += (newSlowTimeCharge - maxSlowTimeCharge);
      }

      power.SetValue((int)slowTimeCharge);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Power"))
    {
      AddSlowTimeCharge();
      Destroy(other.gameObject);
    }
  }
}
