using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public Background[] backgrounds;

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        SetBackgroundPosition();
    }

    private void SetBackgroundPosition()
    {
        foreach (Background bg in backgrounds)
        {
            Vector2 offset;
            offset.x = (_mainCam.transform.position.x - transform.position.x) * bg.values.scrollFactor.x / 1000 + bg.values.offset.x;
            offset.y = 0;
            //            offset.y = (transform.position.y - (_mainCam.transform.position.y + 288)) * bg.values.scrollFactor.y + bg.values.offset.y;

            bg.GetComponent<Renderer>().material.mainTextureOffset = offset;
        }
    }
}
