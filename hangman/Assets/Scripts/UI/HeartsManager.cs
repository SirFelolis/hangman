using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    private int healthPerHeart = 5;

    [SerializeField]
    private GameObject heartSpritePrefab;

    private List<GameObject> heartObjects = new List<GameObject>();

    [SerializeField]
    private Sprite[] heartSprites;

    /// <summary>
    /// Sets the apropriate amount of heart containers based on the max health of the actor calling it.
    /// </summary>
    /// <param name="maxHealth">Max health of the actor calling this function. Must be divisible by 4.</param>
    public void SetHealth( int maxHealth, int currentHealth )
    {
        if (heartObjects != null)
        {
            for (int i = 0; i < heartObjects.Count; i++)
            {
                Destroy(heartObjects[i].gameObject);
            }
            heartObjects.Clear();
        }
        float sum = 0;
        for (int i = 0; i < maxHealth / healthPerHeart; i++)
        {
            var o = Instantiate(heartSpritePrefab, transform, false);
            o.transform.localPosition = new Vector3(i * 0.6f, 0);
            sum += o.transform.localPosition.x;
            heartObjects.Add(o);
        }
        float avg = sum / (maxHealth / healthPerHeart);
        transform.localPosition = new Vector3(-avg, transform.localPosition.y, transform.localPosition.z);

        bool empty = false;
        int j = 0;

        foreach (GameObject heart in heartObjects)
        {
            if (empty)
            {
                heart.GetComponent<SpriteRenderer>().sprite = heartSprites[0];
            }
            else
            {
                j++;
                if (currentHealth >= j * healthPerHeart)
                {
                    heart.GetComponent<SpriteRenderer>().sprite = heartSprites[heartSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = healthPerHeart - (healthPerHeart * j - currentHealth);
                    int healthPerImage = healthPerHeart / heartSprites.Length;
                    int imageIndex = currentHeartHealth / healthPerImage;

                    Debug.Log(currentHealth);
                    Debug.Log(imageIndex);

                    if (imageIndex == 0 && currentHeartHealth > 0)
                    {
                        imageIndex = 1;
                    }

                    heart.GetComponent<SpriteRenderer>().sprite = heartSprites[imageIndex];
                    empty = true;
                }
            }
        }

    }

    private void FixedUpdate()
    {

    }
}
