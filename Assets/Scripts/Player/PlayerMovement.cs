using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Controlls;
using static AudioManager;
using static JuiceUIManager;

public class PlayerMovement : MonoBehaviour
{

  [SerializeField] private float speed = 30;
  [SerializeField] private float dashSpeed = 0;
  private Animator animator;

  public static event Action OnPlayerMove;
  public static event Action OnPlayerDash;

  [SerializeField]
  [Range(0, 5)]
  private float recoilDuration = 0.3f;

  [SerializeField]
  [Range(0, 50)]
  private float recoilIntensity = 0.5f;

  [SerializeField] private AnimationCurve recoilCurve = null;

  private Vector3 recoilDirection = Vector3.zero;
  private float recoilStart = 0;
  private float recoilEnd = 0;

  private Vector3 lastNonZeroInputDirection = Vector3.right;
  [SerializeField] private Transform playerGraphics = null;

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

  private GameObject weapon = null;

  public UISlider power;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    Move();
    LookAtMouse();
    if (Input.GetKeyDown(dashKey)) { Dash(); }
    if (Input.GetKeyDown(slowTimeKey)) { SlowTime(); }

    SlowTimeChargeHandle();
  }

  private void Move()
  {
    transform.position += CalculateVelocity() * speed * Time.deltaTime;
    OnPlayerMove?.Invoke();
  }

  private void LookAtMouse()
  {
    weapon = transform.Find("Weapon").gameObject;
    Vector3 screenMousePosition = new Vector3(Input.mousePosition.x,
                                              Input.mousePosition.y,
                                              -Camera.main.transform.position.z);
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
    mousePosition.z = 0;

    Vector3 vectorToMouse = (mousePosition - weapon.transform.position).normalized;
    weapon.transform.right = vectorToMouse;
  }

  private void Dash()
  {
    if (TweeningOn)
    {
      animator.SetTrigger("IsDashing");
    }
    else
    {
      animator.SetTrigger("IsDashingNoTween");
    }
    OnPlayerDash?.Invoke();
  }

  private Vector3 CalculateVelocity()
  {
    Vector3 inputVelocity = new Vector3(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"),
                                        0).normalized;
    playerGraphics.right = inputVelocity;

    if (inputVelocity.magnitude > 0.01f)
    {
      lastNonZeroInputDirection = inputVelocity;
    }

    Vector3 recoilVelocity = Vector3.zero;
    if (Time.time < recoilEnd)
    {
      float normalizedTime = (Time.time - recoilStart) / recoilDuration;
      recoilVelocity = recoilDirection
          * recoilIntensity
          * Time.deltaTime
          * recoilCurve.Evaluate(normalizedTime);
    }

    Vector3 dashVelocity = lastNonZeroInputDirection
                         * dashSpeed
                         * Time.deltaTime;

    return inputVelocity + recoilVelocity + dashVelocity;
  }

  private void AddRecoil(Vector3 position)
  {
    if (RecoilOn)
    {
      recoilDirection = -transform.right;
      recoilStart = Time.time;
      recoilEnd = recoilStart + recoilDuration;
    }
  }

  private void OnEnable()
  {
    PlayerGun.OnPlayerShoot += AddRecoil;
  }

  private void OnDisable()
  {
    PlayerGun.OnPlayerShoot -= AddRecoil;
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
