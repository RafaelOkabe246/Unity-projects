using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour, IPooledObject
{
    [SerializeField] private SpriteRenderer gfx;
    private float alpha;
    [SerializeField] private float colorChangeSpeed = 2f;
    private Color spriteColor;
    [SerializeField] Color afterImageColor;

    public void OnObjectSpawn()
    {
        alpha = 1f;
    }

    public void SetAfterImageSprite(Sprite sprite, bool flipX, Transform _transform) 
    {
        gfx.sprite = sprite;
        gfx.flipX = flipX;
        transform.localScale = _transform.localScale;
    }

    private void Update()
    {
        spriteColor = new Color(afterImageColor.r, afterImageColor.g, afterImageColor.b, alpha);
        gfx.color = spriteColor;

        alpha -= colorChangeSpeed * Time.deltaTime;
        if (alpha <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
