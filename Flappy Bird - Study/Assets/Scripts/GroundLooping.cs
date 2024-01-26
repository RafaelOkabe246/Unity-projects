using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLooping : MonoBehaviour
{
    public float groundVelocity = 1f;
    public float width = 6f;

    private SpriteRenderer spriteRenderer;

    private Vector2 startSize;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    private void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + groundVelocity * Time.deltaTime, spriteRenderer.size.y);

        if (spriteRenderer.size.x > width)
            spriteRenderer.size = startSize;
    }
}
