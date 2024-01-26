using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyLifes;
    public int pointsValue;
    public float speed;
    public Player player;
    public bool cantMove;
    public WordManager wordManager;
    public ParticleSystem feedbackParticle;
    public GameObject explosion;
    public Animator lightsAnim;
    public GameObject[] words;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator enemyAnim;
    public SpriteRenderer spr;
    public Transform sprParent;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void DescreaseLife()
    {
        enemyLifes -= 1;
    }

    public void DestroyEnemy()
    {
        string message = "Eliminou o malware do tipo " + enemyName + ". Baixando " + pointsValue + " pontos";
        ConsoleManager.consoleManager.CallConsole(message, Color.white, false);
        cantMove = true;
        PointSystem.AddPoints(pointsValue);
        wordManager.RemoveActiveWord();
        GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
        Destroy(gameObject);
    }

    public void CallParticles() {
        feedbackParticle.Play();
    }

    public void CallLightAnimation() {
        lightsAnim.SetTrigger("LightOn");
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) // Block layer
        {
            foreach (GameObject gm in words) 
            {
                gm.SetActive(true);
            }
        }
    }
}
