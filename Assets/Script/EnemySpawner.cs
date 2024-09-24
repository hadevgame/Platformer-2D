using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private float timeUntilSpaw = 10f;
    private GameObject currentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        if (currentEnemy == null)
        {
            timeUntilSpaw -= Time.deltaTime;
            if (timeUntilSpaw <= 0) 
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        timeUntilSpaw = 10f;
    }


}
