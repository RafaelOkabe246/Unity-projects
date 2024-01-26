using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFriction : MonoBehaviour
{
    [SerializeField] private float _coefficientOfFriction;

    [SerializeField] private PhysicMaterial physicMaterial;


    public bool isApplyingForce;
    public const float MOVE_FORCE = 5f;
    private Rigidbody rig;

    private void Update()
    {
        physicMaterial.staticFriction = _coefficientOfFriction;
        physicMaterial.dynamicFriction = _coefficientOfFriction;

    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isApplyingForce)
            rig.AddForce(MOVE_FORCE,0,0);
    }
}
