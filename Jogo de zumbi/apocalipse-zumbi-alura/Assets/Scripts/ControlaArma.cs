using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletPoint;
    public AudioClip SomDoTiro;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, BulletPoint.transform.position, BulletPoint.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
    }
}
