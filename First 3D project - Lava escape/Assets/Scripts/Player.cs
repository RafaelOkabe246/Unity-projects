using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] internal Player_movement Player_move;
    [SerializeField] internal CharacterController _CharacterController;

    //Movement variables
    [Header("Movement variables")]
    [SerializeField] internal Rigidbody Rig;
    [SerializeField] internal Transform Cam;



}
