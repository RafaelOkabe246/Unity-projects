using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSight : MonoBehaviour
{
    public LayerMask Player;
    public float Range;  
    public bool PlayerInSight;

    public Transform Target;

    [Header("Animator")]
    public Animator _Animator;

    [Header("Game Objects")]
    public GameObject TurretTop;
    public GameObject Bullet;

    public Transform ShootPoint;

    public float FireRate;
    float nextTimeToFire = 0;
    public float Force;


    Vector2 Direction;

    public GameObject AlarmLight;

    void Start()
    {
        _Animator = this.GetComponent<Animator>();
        //Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        Finding_player();

        //Vector2 targetPos = Target.position;

        //Direction = targetPos - (Vector2)TurretTop.transform.position;

        //RaycastHit2D rayInfo = Physics2D.Raycast(TurretTop.transform.position, Direction, Range, Player);

        //if(rayInfo == true)
        //{
          //  if (rayInfo.collider.gameObject.tag == "Player")
            //{
              //  if(PlayerInSight == false)
                //{
                  //  PlayerInSight = true;
                    //AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                //}
            //}
            //else 
            //{
              //  if (PlayerInSight == true)
                //{
                  //  PlayerInSight = false;
                    //AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
              //  }
          //  }
        //}
        //else
        //{
            //Vector3 rotação_inicial = new Vector3(0,0,0);
          //  TurretTop.transform.rotation = Quaternion.Lerp(TurretTop.transform.rotation, Quaternion.identity, Time.deltaTime );
        //}

        //if (PlayerInSight == true)
        //{
          //  TurretTop.transform.right = -Direction;
           // if(Time.time > nextTimeToFire)
            //{
              //  nextTimeToFire = Time.time + 1 / FireRate;
                //Shoot();
            //}
        //}
    }

    void Finding_player()
    {
        if (Vector2.Distance(TurretTop.transform.position, Target.position) < Range)
        {
            PlayerInSight = true;
        }
        else
        {
            PlayerInSight = false;
        }
    }

    void Shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            _Animator.SetTrigger("Hit");
        }
    }
}
