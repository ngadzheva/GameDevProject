using System.Collections;
using System.Collections.Generic;
using static AudioManager;
using static ScreenShaker;
using static JuiceUIManager;
using UnityEngine;
using UnityEditor.UIElements;
using TMPro;
using System;

public class Bullet : MonoBehaviour
{

  [SerializeField] private GameObject hitParticles = null;
  [SerializeField] private GameObject trailParticles = null;
  [SerializeField] private float speed = 2;
  [SerializeField] private bool damagePlayer = false;
  [SerializeField] private bool damageEnemy = true;

  [SerializeField]
  [Range(0, 10)]
  private float scale = 5f;

  public Vector3 MoveDirection { get; set; } = Vector3.zero;
  private bool hasHit = false;

  public static Func<float> TimeModif;

  private void Start()
  {
    hitParticles = transform.GetChild(0).gameObject;
    trailParticles.SetActive(ParticlesOn);
  }

  private void Update()
  {
    float timeModif = 1f;
    float? newTineModif = TimeModif?.Invoke();
    if (newTineModif.HasValue)
    {
      timeModif = newTineModif.Value;
    }
    transform.position += MoveDirection * speed * timeModif * Time.deltaTime;
    //transform.localScale = MoveDirection * scale;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if ((collision.CompareTag("Enemy") && gameObject.CompareTag("Bullet_Player") || collision.CompareTag("Player") && gameObject.CompareTag("Bullet_Enemy")) /*&& !hasHit*/)
    {
      hasHit = true;

      GetComponent<SpriteRenderer>().enabled = false;
      MoveDirection = Vector3.zero;
      if (ParticlesOn) { hitParticles.SetActive(true); }
      PlayHitSound();
      // ShakeScreenLight();

      Destroy(gameObject, 1);
    }
  }


}
