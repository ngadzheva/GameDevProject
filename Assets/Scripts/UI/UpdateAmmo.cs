using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerWeaponInventory;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateAmmo : MonoBehaviour
{
  [SerializeField]
  [Range(0, 20)]
  protected int initialScore = 10;
  private TextMeshProUGUI scoreText = null;

  private void Start() {
    scoreText.text = initialScore.ToString();
  }

  private void Update() {
  }

  private void UpdatePistolAmmoUI() {
    scoreText.text = Ammo1.ToString();
  }

  private void UpdateShotGunAmmoUI() {
    scoreText.text = Ammo2.ToString();
  }

  private void UpdateSwordAmmoUI() {
    scoreText.text = Ammo3.ToString();
  }
}
