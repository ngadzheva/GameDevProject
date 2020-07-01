using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ScoreSystem;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WonPoints : MonoBehaviour
{
  [SerializeField]
  [Range(0, 20)]
  protected int initialScore = 0;
  public TextMeshProUGUI scoreText = null;

  private void Start() {
    scoreText.text = GetWonScore().ToString();
  }
}
