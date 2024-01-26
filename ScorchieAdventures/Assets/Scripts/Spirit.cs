using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private SpriteRenderer spr;
    public float speed;
    public float transparencySpeed;
    private float alpha = 1f;
    private bool canDecreaseAlpha = false;
    private Vector3 initialPosition;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = initialPosition;
        alpha = 1f;
        canDecreaseAlpha = false;
        StartCoroutine(StartDecreasingAlpha());
    }

    private void Update()
    {
        if (canDecreaseAlpha)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;

            alpha -= transparencySpeed * Time.deltaTime;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
            if (alpha <= 0f)
                Destroy(gameObject);
        }
    }

    private IEnumerator StartDecreasingAlpha() 
    {
        yield return new WaitForSeconds(1.1f);

        canDecreaseAlpha = true;
    }
}
