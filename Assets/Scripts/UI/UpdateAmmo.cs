using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateAmmo : MonoBehaviour
{
  [SerializeField]
  [Range(0, 20)]
  protected int initialScore = 10;
  public TextMeshProUGUI scoreText = null;

  private void Start() {
    scoreText.text = initialScore.ToString();
  }

  public void UpdateAmmoUI(int ammo) {
    scoreText.text = ammo.ToString();
  }
}
