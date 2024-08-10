using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1;

    [SerializeField]
    private float minSpawTime;

    [SerializeField]
    private float maxSpawTime;

    private float timeUntilSpaw;


    void Awake()
    {
        SetTimeUntilSpaw();
    }

    void Update()
    {
        timeUntilSpaw -= Time.deltaTime;


        if (timeUntilSpaw <= 0)
        {
            Instantiate(enemy1, transform.position, Quaternion.identity);
            SetTimeUntilSpaw();

        }
    }

    private void SetTimeUntilSpaw()
    {
        timeUntilSpaw = Random.Range(minSpawTime, maxSpawTime);
    }
}
