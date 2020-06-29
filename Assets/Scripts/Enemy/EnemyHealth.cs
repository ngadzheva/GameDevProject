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

    public static event Action<Vector3> OnEnemyDeath;

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
        healthBar.SetHealth(hp);
        if (hp <= 0) {
            PlayDeathSound();
            ShakeScreenHeavy();
            OnEnemyDeath?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}
