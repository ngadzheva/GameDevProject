using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  void Start()
  {
    Instance = this;
  }

  IEnumerator EndGame()
  {
    yield return new WaitForSeconds(0.5f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
