using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour {

    [SerializeField] private GameObject[] awards = null;

    private void Start() {
        EnemyHealth.OnEnemyDeath += SpawnAward;
    }

    public void SpawnAward(Vector3 position) {
        int awardIndex = Random.Range(0, awards.Length);
        Instantiate(awards[awardIndex], position, Quaternion.Euler(0, 0, 0));
    }
}
