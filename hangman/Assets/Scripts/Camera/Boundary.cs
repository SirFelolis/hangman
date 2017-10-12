using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boundary : MonoBehaviour
{
    private BoxCollider2D box2D;

    private CameraController cameraController;

    private Vector2 boundarySizeController1;
    private Vector2 boundarySizeController2;

    [SerializeField]
    private Transform handle1;
    [SerializeField]
    private Transform handle2;

    void Awake()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        box2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        boundarySizeController1 = handle1.localPosition;
        boundarySizeController2 = handle2.localPosition;

        Bounds b = new Bounds();

        b.SetMinMax(
            new Vector2(Mathf.Min(boundarySizeController1.x, boundarySizeController2.x), Mathf.Min(boundarySizeController1.y, boundarySizeController2.y)),
            new Vector2(Mathf.Max(boundarySizeController1.x, boundarySizeController2.x), Mathf.Max(boundarySizeController1.y, boundarySizeController2.y))
            );

        box2D.offset = b.center;
        box2D.size = b.size;
    }

    private void OnTriggerStay2D( Collider2D col )
    {
        if (col.CompareTag("Player"))
        {
            cameraController.currentBoundary = box2D;
        }
    }
}
