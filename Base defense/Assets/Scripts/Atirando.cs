using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirando : MonoBehaviour
{
    private Animator _Animator;
    public bool isShooting;
    public bool isReloading;

    public Transform Mira;
    public GameObject BulletPrefab;

    public int Ammunition;

    void Start()
    {
        Ammunition = 5;
        _Animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isShooting == false && Ammunition > 0 && isReloading == false)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && Ammunition < 5 && isReloading == false && isShooting == false)
        {
            Reload();
        }

        _Animator.SetBool("isShooting", isShooting);
        _Animator.SetBool("isReloading", isReloading);
    }

    void Shoot()
    {
        Ammunition -= 1;
        isShooting = true;
        Instantiate(BulletPrefab, Mira.position, BulletPrefab.transform.rotation);

    }

    void StopShoot()
    {
        isShooting = false;
    }


    void Reload()
    {
        Ammunition = 5;
        isReloading = true;
    }
    void StopReload()
    {
        isReloading = false;
    }
}
