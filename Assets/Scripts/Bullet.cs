using System.Collections;
using System.Collections.Generic;
using static AudioManager;
using UnityEngine;
using UnityEditor.UIElements;
using TMPro;
using System;

public class Bullet : MonoBehaviour
{
  [SerializeField] private float speed = 2;
  [SerializeField] private bool damagePlayer = false;
  [SerializeField] private bool damageEnemy = true;

  public Vector3 MoveDirection { get; set; } = Vector3.zero;
  private bool hasHit = false;

  public static Func<float> TimeModif;

  private void Update()
  {
    float timeModif = 1f;
    float? newTineModif = TimeModif?.Invoke();
    if (newTineModif.HasValue)
    {
      timeModif = newTineModif.Value;
    }
    transform.position += MoveDirection * speed * timeModif * Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if ((collision.CompareTag("Enemy") && gameObject.CompareTag("Bullet_Player") || collision.CompareTag("Player") && gameObject.CompareTag("Bullet_Enemy")) /*&& !hasHit*/)
    {
      hasHit = true;

      GetComponent<SpriteRenderer>().enabled = false;
      MoveDirection = Vector3.zero;

      PlayHitSound();

      Destroy(gameObject, 1);
    }
  }


}
