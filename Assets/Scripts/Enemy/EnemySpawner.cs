using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public static Func<float> TimeModif;
    private GameObject spawningArea;

    void Start() {
        StartCoroutine(SpawnEnemies());
        player = GameObject.FindWithTag("Player").transform;
        spawningArea = transform.GetChild(0).gameObject;
    }

    private IEnumerator SpawnEnemies() {
        while (true) {

            float timeModif = 1f;
            float? newTineModif = TimeModif?.Invoke();
            if (newTineModif.HasValue)
            {
                timeModif = newTineModif.Value;
            }

            yield return new WaitForSeconds(1f / spawnRate / timeModif);

            //Vector3 spawnLocation = player.position 
            //    + (Vector3)UnityEngine.Random.insideUnitCircle.normalized * 50;

            RectTransform spawningRect = spawningArea.GetComponent<RectTransform>();


            Vector3 spawnLocation = new Vector3 ((UnityEngine.Random.value - 0.5f) * spawningRect.rect.width, (UnityEngine.Random.value - 0.5f) * spawningRect.rect.height, 0f);

            Instantiate(ChooseEnemy(),
                spawnLocation, 
                Quaternion.identity);
        }
    }

    private GameObject ChooseEnemy()
    {
        GameObject result = enemy1;

        float rand = UnityEngine.Random.value;
        if (rand <= 0.7f) { result = enemy1; }
        else if (rand <= 0.8f) { result = enemy2; }
        else if (rand <= 1.0f) { result = enemy3; }

        return result;
    }
}
