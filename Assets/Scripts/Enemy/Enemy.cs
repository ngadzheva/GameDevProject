using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
  [SerializeField] private GameObject bullet = null;
  [SerializeField] private PlayerGun gun = null;
  private Transform bulletSpawnLocation = null;

  [SerializeField] private float speed = 10;
  [SerializeField] public float shootRecharge = 2;
  private Transform player;

  public static Func<float> TimeModif;

  private Animator animator;

  void Start()
  {
    player = GameObject.FindWithTag("Player").transform;
    bulletSpawnLocation = transform.GetChild(0);
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    LookAtPlayer();
    AimAtPlayer();
  }

  public void MoveToPosition(Vector3 targetPosition)
  {
    float timeModif = 1f;
    float? newTineModif = TimeModif?.Invoke();
    if (newTineModif.HasValue)
    {
      timeModif = newTineModif.Value;
    }

    Vector3 VectorToTarget = (targetPosition - transform.position).normalized;
    transform.position += VectorToTarget * Time.deltaTime * speed * timeModif;
  }

  public void Shoot()
  {
    if (gun == null)
    {
      Debug.LogError("No gun found");
    }
    else
    {
      gun.Shoot();
    }
  }

  private void LookAtPlayer()
  {
    if (player == null)
    {
      Debug.LogError("No player found");
    }
    else
    {
      SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();

      if ((player.position - transform.position).normalized.x < 0)
      {
        spriteRenderer.flipX = true;
      }
      else
      {
        spriteRenderer.flipX = false;
      }
    }
  }

  private void AimAtPlayer()
  {

    if (player == null)
    {
      Debug.LogError("No player found");
    }
    else
    {
      Transform weaponTransform = transform.Find("Weapon");
      Vector3 vectorToPlayer = (player.position - weaponTransform.position).normalized;
      weaponTransform.right = vectorToPlayer;
    }

  }
}
