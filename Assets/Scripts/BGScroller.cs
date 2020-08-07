using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;

    private Vector3 startPosition;
    private float tileSize;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        tileSize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
