using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour
{
    public bool isDestroyed;
    public float HP;
    public float Shield;

    [Header("UI")]
    public Image LifeBar;
    public Image ShieldBar;

    [Header("Sprites")]
    public Sprite[] buildingSprites;
    public SpriteRenderer currentSprite;

    private void Start()
    {
        HP = buildingSprites.Length;
        currentSprite.sprite = buildingSprites[buildingSprites.Length - 1];//[buildingSprites.Length];
    }

    private void Update()
    {
        if (HP <= 0)
        {
            //Building is destroyed
            isDestroyed = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            UpdateSprite();
        }
    }

    public void UpdateSprite()
    {
        currentSprite.sprite = buildingSprites[(int)HP];
    }
}
