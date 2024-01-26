using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede_movel : MonoBehaviour
{
    [SerializeField] internal Transform direction;



    public void Mover()
    {
        transform.position = Vector2.Lerp(transform.position, direction.position,Time.deltaTime);
    }
}
