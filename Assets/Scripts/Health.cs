using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Mathf;
using static AudioManager;
using static ScreenShaker;

public class Health : MonoBehaviour {

	[SerializeField]
  [Range(1, 5)]
	private int health = 5;

  [SerializeField]
  private string tagName = "Bullet";

  public Healthbar healthBar;
  public ParticleSystem blood;
	private Animator animator;
	public GameObject cross;

  public static event Action<Vector3> OnEnemyDeath;

	void Start() {
		animator = GetComponent<Animator>();
    healthBar.SetMaxHealth(health);
	}

	public void Die() {
		PlayDeathSound();
    ShakeScreenHeavy();
		Destroy(gameObject);
	}

	public void TakeDamage() {
		int damage = 1;
		health = Max(health - damage, 0);
		animator.SetInteger("Health", health);
		animator.SetTrigger("TookDamage");
    healthBar.SetHealth(health);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.transform.parent != transform
			&& collision.CompareTag(tagName)) {

			TakeDamage();
			PlayEffects();
		}
	}

	private void PlayEffects() {
		blood.Stop();
		blood.Play();

		ShakeScreenLight();
	}
}
