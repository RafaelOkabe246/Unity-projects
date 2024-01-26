using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour, IPooledObject
{
    public SpriteRenderer gfx;
    public Sprite[] possibleSprites;
    public float minScale = 1f;
    public float maxScale = 3f;
    private int spriteToGetIndex;
    public float minSpeed = 1f;
    public float maxSpeed = 10f;
    private float speed;
    [SerializeField] private bool isFromObjectPool = true;

    private void Start()
    {
        if (!isFromObjectPool) 
        {
            spriteToGetIndex = Random.Range(0, possibleSprites.Length);
            gfx.sprite = possibleSprites[spriteToGetIndex];

            float newScale = Random.Range(minScale, maxScale);
            transform.localScale = Vector3.one * newScale;

            speed = Random.Range(minSpeed, maxSpeed);

            Destroy(gameObject, 30f);
        }
    }

    public void OnObjectSpawn()
    {
        spriteToGetIndex = Random.Range(0, possibleSprites.Length);
        gfx.sprite = possibleSprites[spriteToGetIndex];

        float newScale = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * newScale;

        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
