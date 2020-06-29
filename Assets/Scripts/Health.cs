using UnityEngine;
using static UnityEngine.Mathf;
// using static AudioManager;

public class Health : MonoBehaviour {

	[SerializeField]
	private int health = 100;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float duration = 0.15f;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float magnitude = 0.1f;

	// public ParticleSystem blood;
	// public ScreenShaker screenShaker;
	private Animator animator;
	public GameObject cross;

	void Start() {
		animator = GetComponent<Animator>();
	}

	public void SpawnCross() {
		Vector2 spawnPosition = new Vector2 {
			x = transform.position.x,
			y = -0.1f
		};
		Instantiate(cross, spawnPosition, Quaternion.identity);
	}

	public void Die() {
		// screenShaker.StartShake(duration, magnitude);
		Destroy(gameObject);
	}

	public void TakeDamage() {
		int damage = 10;
		health = Max(health - damage, 0);
		animator.SetInteger("Health", health);
		animator.SetTrigger("TookDamage");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.transform.parent != transform
			&& collision.gameObject.CompareTag("Fireball")) {

			TakeDamage();
			// PlayEffects();
		}
	}

	// private void PlayEffects() {
	// 	blood.Stop();
	// 	blood.Play();

	// 	screenShaker.StartShake(duration, magnitude);
	// 	PlayPunchedSound();
	// }
}
