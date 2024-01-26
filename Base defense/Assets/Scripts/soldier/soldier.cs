using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldier : MonoBehaviour
{
    [SerializeField] internal soldier_shoot Shoot_script;

    [SerializeField] internal Soldier_position Movement_script;

    [SerializeField] internal soldier_mode State_script;

    [SerializeField] internal soldier_field_of_view Field_of_view_script;

    [SerializeField] internal soldier_enemy_detection detect_enemy_script;

    //Components variables
    internal Rigidbody2D Rig;
    internal Animator Anim;

    //Movement variables
    float speed_movement = 10f;


    //Shooting variables
    public GameObject Bullet;
    public LayerMask Enemy = 10;

    //State variables
    bool isShooting;
    bool isMoving;
    bool isSelected;
    bool isInteracting;


    private void Update()
    {
        //Vision_camp_script.loading_cone();
    }
}
