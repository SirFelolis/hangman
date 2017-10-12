﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private bool respawn = true;

    private bool doSpawnEnemy = true;

    private GameObject enemyInstance;

    private void FixedUpdate()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if ((viewPos.x < 1 && viewPos.x > 0) && (viewPos.y < 1 && viewPos.y > 0) && viewPos.z > 0)
        {
            if (doSpawnEnemy)
            {
                SpawnEnemy();
                if (!respawn)
                    Destroy(gameObject);
            }
        }
        else
        {
            doSpawnEnemy = true;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyInstance == null)
        {
            enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
            doSpawnEnemy = false;
        }
/*        else if (!enemyInstance.activeSelf)
        {
            enemyInstance.SetActive(true);
            enemyInstance.transform.position = transform.position;
            enemyInstance.GetComponent<GroundEnemy>().Reset();
            doSpawnEnemy = false;
        }*/
    }
}