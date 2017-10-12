using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float smooth;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private float lookAheadDistanceX;

    private float targetLookAheadX;
    private float lookAheadDirX;

    [SerializeField]
    private Vector2 focusAreaSize;

    private FocusArea focusArea;

    public BoxCollider2D currentBoundary;

    private BoxCollider2D box2D;

    private struct FocusArea
    {
        public Vector2 centre, velocity;
        float left, right, top, bottom;

        public FocusArea( Bounds targetBounds, Vector2 size )
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2();
        }

        public void Update( Bounds targetBounds )
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        focusArea = new FocusArea(player.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
        box2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        box2D.size = new Vector2(Camera.main.orthographicSize * 3.5586f, Camera.main.orthographicSize * 2);

        transform.position = new Vector2(player.position.x, player.position.y);
    }

    private void LateUpdate()
    {
        focusArea.Update(player.GetComponent<BoxCollider2D>().bounds);

        FollowPlayer();

        if (currentBoundary != null)
            LimitBounds();
    }

    private void LimitBounds()
    {
        /*        if (box2D.size.x > currentBoundary.size.x)
                {
                    Camera.main.orthographicSize -= 0.01f;

                    box2D.size = new Vector2(Camera.main.orthographicSize * 3.5586f, Camera.main.orthographicSize * 2);

                    return;
                }

                if (box2D.size.y > currentBoundary.size.y)
                {
                    Camera.main.orthographicSize -= 0.01f;

                    box2D.size = new Vector2(Camera.main.orthographicSize * 3.5586f, Camera.main.orthographicSize * 2);

                    return;
                }*/

        float desiredXPosition = transform.position.x;
        if (box2D.size.x <= currentBoundary.size.x)
        {
            desiredXPosition = Mathf.Clamp(transform.position.x, currentBoundary.bounds.min.x + box2D.size.x / 2, currentBoundary.bounds.max.x - box2D.size.x / 2);
        }
        else
        {
            desiredXPosition = ((currentBoundary.bounds.min.x + box2D.size.x / 2) + (currentBoundary.bounds.max.x - box2D.size.x / 2)) / 2;
        }

        float desiredYPosition = transform.position.y;
        if (box2D.size.y <= currentBoundary.size.y)
        {
            desiredYPosition = Mathf.Clamp(transform.position.y, currentBoundary.bounds.min.y + box2D.size.y / 2, currentBoundary.bounds.max.y - box2D.size.y / 2);
        }
        else
        {
            desiredYPosition = ((currentBoundary.bounds.min.y + box2D.size.y / 2) + (currentBoundary.bounds.max.y - box2D.size.y / 2)) / 2;
        }

        transform.position = new Vector3(desiredXPosition, desiredYPosition, transform.position.z);
    }

    private void FollowPlayer()
    {
        Vector2 focusPosition = focusArea.centre + offset;

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
        }

        targetLookAheadX = lookAheadDirX * lookAheadDistanceX;

        focusPosition += Vector2.right * targetLookAheadX;

        transform.position = Vector3.Lerp(transform.position, (Vector3)focusPosition + Vector3.forward * -10, smooth * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }
}
