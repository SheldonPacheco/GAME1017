using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private float backgroundPosition;
    private float originalPosition;
    private int direction = 1; // 1 for right, -1 for left
    public float sceneEndX = -7.22f;
    public float resetX = -7.0f; //end of level, then go backwards
    public float imageLength = 22.40287f;

    void Start()
    {
        backgroundPosition = transform.position.x;
        originalPosition = backgroundPosition;
    }

    void Update()
    {
        backgroundPosition = transform.position.x;

        if (backgroundPosition > sceneEndX)
        {
            direction = -1; //changes direction to move towards resetX
        }
        else if (backgroundPosition < originalPosition)
        {
            direction = 1; //changes direction to move in the original direction
        }

        transform.Translate(Vector2.right * Time.deltaTime * scrollSpeed * direction);
    }
}