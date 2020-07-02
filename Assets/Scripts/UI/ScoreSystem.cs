using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreSystem : MonoBehaviour
{

  private Animator animator;
  private TextMeshProUGUI scoreText = null;
  private static int score = 0;

  private void Start()
  {
    animator = GetComponent<Animator>();
    scoreText = GetComponent<TextMeshProUGUI>();
    scoreText.text = score.ToString();
    EnemyHealth.OnEnemyDeath += UpdateScoreUI;
  }

  private void UpdateScoreUI(Vector3 position)
  {
    score += 5;
    scoreText.text = score.ToString();
  }

  public static int GetWonScore()
  {
    return score;
  }
}
