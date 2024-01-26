using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCatapult : MonoBehaviour
{
    public Rigidbody rigBall;
    public Transform launchPoint;

    [Range(0,90)]
    public float angle;
    public float power;

    private float _time;
    private bool _isLaunched;

    public Vector2 _initalVelocity;
    public Vector2 _initalPos;

    public PhysicsControls physicsActions;

    // Start is called before the first frame update
    void Awake()
    {
        physicsActions = new PhysicsControls();
    }

    // Update is called once per frame
    void OnEnable()
    {
        physicsActions.Enable();
    }

    private void OnDisable()
    {
        physicsActions.Disable();
    }

    private void Update()
    {
        if (physicsActions.Catapult.Launch.WasPressedThisFrame())
        {
            LaunchBall();
        }
        if (physicsActions.Catapult.Reset.WasPressedThisFrame())
        {
            ResetAll();
        }

        if (_isLaunched)
        {
            _time += Time.deltaTime;

            //X pos changing
            float newBallX = KinematicEquation(0, _time, _initalVelocity.x, _initalPos.x);

            //Y pos changing
            float newBallY = KinematicEquation(-9.81f, _time, _initalVelocity.y, _initalPos.y);


            rigBall.position = new Vector3(newBallX, newBallY, rigBall.position.z);
        }

    }

    void ResetAll()
    {
        rigBall.gameObject.transform.position = launchPoint.position;
        _initalPos = Vector2.zero;
        _initalVelocity = Vector2.zero;
        _isLaunched = false;
        _time = 0;
    }

    void LaunchBall()
    {
        rigBall.gameObject.transform.position = launchPoint.position;

        //Set inital position and velocity
        _initalVelocity = new Vector2(Mathf.Cos(angle * Mathf.PI /180) , Mathf.Sin(angle * Mathf.PI / 180)) * power;
        _initalPos = new Vector2(rigBall.position.x, rigBall.position.y);
         
        _isLaunched = true;
    }

    float KinematicEquation(float acceleration, float time, float initalVelocity, float initalPos)
    {
        return 0.5f * acceleration * time * time  + initalVelocity * time + initalPos;
    }
}
