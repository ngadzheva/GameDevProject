using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour
{

  [SerializeField] private GameObject[] awards = null;
  private bool spawnedAward = false;

  private void Start()
  {
    EnemyHealth.OnEnemyDeath += SpawnAward;
  }

  public void SpawnAward(Vector3 position)
  {
    int awardIndex = Random.Range(0, awards.Length);
    if (!spawnedAward)
    {
      Instantiate(awards[awardIndex], position, Quaternion.Euler(0, 0, 0));
      spawnedAward = true;
    }
  }
}
