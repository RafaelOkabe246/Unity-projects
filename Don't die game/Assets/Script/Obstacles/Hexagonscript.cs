using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagonscript : MonoBehaviour
{
    private GameController _GameController;
    public Rigidbody2D rb;

    public float shrinkspeed;

    void Start()
    {
        rb.rotation = Random.Range(0f, 360f);
        transform.localScale = Vector3.one * 10f;
    }

    void Update()
    {
        transform.localScale -= Vector3.one * shrinkspeed * Time.deltaTime;

        if(transform.localScale.x <= 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}