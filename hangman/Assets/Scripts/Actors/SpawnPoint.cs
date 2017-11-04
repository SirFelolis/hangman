using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemy;

    public bool respawn = true;

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
        else if (enemyInstance == null)
        {
            doSpawnEnemy = true;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyInstance == null)
        {
            enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyInstance.GetComponent<StateController>().SetupAI(true);
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