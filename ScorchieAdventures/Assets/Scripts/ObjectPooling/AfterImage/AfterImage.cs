using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour, IPooledObject
{
    private SpriteRenderer spr;
    private PlayerAnimationManager playerAnim;
    private float alpha;
    [SerializeField] private float colorChangeSpeed = 2f;
    private Color spriteColor;
    [SerializeField] Color afterImageColor;

    public void OnObjectSpawn()
    {
        alpha = 1f;
        spr.sprite = playerAnim.gfx.sprite;
        transform.localScale = playerAnim.gameObject.transform.localScale;
        transform.localRotation = playerAnim.gfx.transform.rotation;
    }

    private void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();
        playerAnim = FindObjectOfType<PlayerAnimationManager>();
    }

    void Update()
    {
        spriteColor = new Color(afterImageColor.r, afterImageColor.g, afterImageColor.b, alpha);
        spr.color = spriteColor;

        alpha -= colorChangeSpeed * Time.deltaTime;
        if (alpha <= 0f) 
        {
            gameObject.SetActive(false);
        }
    }
}
