using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource hitSound = null;
    [SerializeField] private AudioSource gunFireSound = null;
    [SerializeField] private AudioSource deathSound = null;

    private static AudioManager instance;

    private void Start() {
        instance = this;
    }

    public static void PlayHitSound() {
        instance.hitSound.Play();
    }

    public static void PlayGunfireSound() {
        instance.gunFireSound.Play();
    }

    public static void PlayDeathSound() {
        instance.deathSound.Play();
    }
}
