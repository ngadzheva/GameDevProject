using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemy1 = null;
    [SerializeField] private GameObject enemy2 = null;
    [SerializeField] private GameObject enemy3 = null;
    //[SerializeField] private GameObject enemy4 = null;
    //[SerializeField] private GameObject enemy5 = null;

    [SerializeField]
    [Range(0, 10)]
    private float spawnRate = 1.5f;

    private Transform player;

    void Start() {
        StartCoroutine(SpawnEnemies());
        player = GameObject.FindWithTag("Player").transform;
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(1f / spawnRate);

            Vector3 spawnLocation = player.position 
                + (Vector3)Random.insideUnitCircle.normalized * 50;
            Instantiate(ChooseEnemy(),
                spawnLocation, 
                Quaternion.identity);
        }
    }

    private GameObject ChooseEnemy()
    {
        GameObject result = enemy1;

        float rand = Random.value;
        if (rand <= 0.7f) { result = enemy1; }
        else if (rand <= 0.8f) { result = enemy2; }
        else if (rand <= 1.0f) { result = enemy3; }

        return result;
    }
}
