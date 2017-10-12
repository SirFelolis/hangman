using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelTrigger : TriggerBase
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private bool countAsDungeonClear = false;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.CompareTag("Player"))
        {
            if (countAsDungeonClear)
                GameInformant.dungeonsCleared = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
