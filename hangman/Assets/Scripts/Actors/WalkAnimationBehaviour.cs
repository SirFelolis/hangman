using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimationBehaviour : MonoBehaviour
{
    public GameObject[] feet;

    private Vector2[] localFeetTargets;
    private Vector2[] worldFeetTargets;

    private float lastMoveTime;

    public float feetSpeedMultiplier = 5f;

    public float legOffsetTime = 0.2f;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        localFeetTargets = new Vector2[feet.Length];
        for (int i = 0; i < feet.Length; i++)
        {
            localFeetTargets[i] = feet[i].transform.position - transform.position;
        }

        worldFeetTargets = new Vector2[localFeetTargets.Length];
        UpdateWorldFeetTargets();

    }

    private void Update()
    {
        UpdateWorldFeetTargets();

        MoveFeet();
    }

    private void MoveFeet()
    {
        for (int i = 0; i < feet.Length; i++)
        {

            if ((feet[i].transform.position - (Vector3)worldFeetTargets[i]).magnitude > 8f && Time.time - lastMoveTime > legOffsetTime)
            {
                lastMoveTime = Time.time;
                StopCoroutine("MoveFoot");
                StartCoroutine(MoveFoot(i));
                //                feet[i].transform.position = worldFeetTargets[i];
            }
        }
    }

    private IEnumerator MoveFoot( int i )
    {
        Vector2 feetPos = feet[i].transform.position;
        Vector2 target = worldFeetTargets[i];
        float lerpValue = 0;

        while (lerpValue < 1.0f)
        {
            lerpValue += Time.deltaTime * feetSpeedMultiplier;

            feetPos.x = Mathf.Lerp(feetPos.x, target.x + _rb2d.velocity.x / 8, lerpValue); // That "8" value HAS TO BE THERE. And I don't know why. Sorry future-me.

            feetPos.y = Mathf.Sin(lerpValue * Mathf.PI) + target.y;

            feet[i].transform.position = feetPos;

            yield return null;
        }
    }

    private void UpdateWorldFeetTargets()
    {
        for (int i = 0; i < localFeetTargets.Length; i++)
        {
            worldFeetTargets[i] = localFeetTargets[i] + (Vector2)transform.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (localFeetTargets != null)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < localFeetTargets.Length; i++)
            {
                Gizmos.DrawWireSphere(worldFeetTargets[i], 8);
            }
        }
    }
}
