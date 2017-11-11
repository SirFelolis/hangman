using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int prevSceneIndex;

    public bool inOverworld;

    [SerializeField]
    private Vector2[] overworldStartingPositions;

    private GameObject player;

    public List<GameObject> globalListEnemies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
        {
            //            Debug.LogWarning("Too many game managers!");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

    }

    private void Start()
    {
        FindEnemies();
    }

    private void FixedUpdate()
    {
        foreach (GameObject enemy in globalListEnemies)
        {
            if (enemy == null)
            {
                globalListEnemies.Remove(enemy);
                return;
            }

            Vector3 viewPos = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if ((viewPos.x > 1.05f || viewPos.x < -0.05f) || (viewPos.y > 1.05f || viewPos.y < -0.05f))
            {
                Destroy(enemy);
            }
        }
    }

    private void SceneManager_activeSceneChanged( Scene arg0, Scene arg1 )
    {
        // Dump and refill list of enemies
        FindEnemies();

        // Find player again
        player = GameObject.FindGameObjectWithTag("Player");

        if (SceneManager.GetActiveScene().name == "Overworld01")
        {
            player.transform.position = overworldStartingPositions[prevSceneIndex];
            prevSceneIndex = SceneManager.GetActiveScene().buildIndex;


            inOverworld = true;
        }
        else
            inOverworld = false;

        prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void FindEnemies()
    {
        globalListEnemies.Clear();

        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < foundEnemies.Length; i++)
        {
            globalListEnemies.Add(foundEnemies[i]);
        }
    }

    private void OnDrawGizmos()
    {
        if (SceneManager.GetActiveScene().name == "Overworld01")
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < overworldStartingPositions.Length; i++)
            {
                Gizmos.DrawWireSphere(overworldStartingPositions[i], 1);
            }
        }
    }
}
