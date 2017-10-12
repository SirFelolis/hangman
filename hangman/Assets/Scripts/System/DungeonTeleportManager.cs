using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTeleportManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelPortals;

    private void Awake()
    {
        // Delete the level portal the player is standing on first.
        if (GameInformant.dungeonsCleared > 0)
            Destroy(levelPortals[GameInformant.dungeonsCleared - 1]);

        // Then delete the rest.
        for (int i = 0; i < GameInformant.dungeonsCleared - 1; i++)
        {
            Destroy(levelPortals[i]);
        }
    }
}
