using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AudioManager;
using static ScreenShaker;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    [Range(1, 5)]
    protected int hp = 3;

    [SerializeField]
	[Range(0.0f, 1.0f)]
	private float duration = 0.15f;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float magnitude = 0.1f;

    public static event Action<Vector3> OnEnemyDeath;
    public ScreenShaker screenShaker;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet_Player")) {
            TakeDamage();
        }
    }

    protected void TakeDamage() {
        hp -= 1;
        animator.SetTrigger("TookDamage");
        animator.SetInteger("Health", hp);
        screenShaker.StartShake(duration, magnitude);
        PlayHitSound();
        if (hp <= 0) {
            PlayDeathSound();
            screenShaker.StartShake(duration, magnitude);
            OnEnemyDeath?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}
