using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public void Start() {
    PlayerHealth.OnPlayerDeath += EndGame;
  }
  public void EndGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
