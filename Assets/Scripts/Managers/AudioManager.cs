using UnityEngine;
using static JuiceUIManager;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource hitSound = null;
    [SerializeField] private AudioSource gunFireSound = null;
    [SerializeField] private AudioSource music = null;
    [SerializeField] private AudioSource dashSound = null;
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

    public static void SwitchMusicOnOrOff(bool IsOn) {
        if (IsOn) {
            instance.music.Play();
        } else {
            instance.music.Stop();
        }
    }

    public static void PlayDashSound() {
        if (SoundOn) { instance.dashSound.Play(); }
    }

    public static void PlayDeathSound() {
        instance.deathSound.Play();
    }
}
