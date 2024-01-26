using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public Camera CameraMain;
    private Vector2 Target;
    [SerializeField] private GameSystem GS;

    [Header("Missle")]
    public GameObject MissleTarget;

    [Header("Inputs")]
    public PlayerControls playerControls;
    private InputAction Shooting;
    private InputAction MovingTarget;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        MovingTarget = playerControls.UI.Point;
        MovingTarget.Enable();
        MovingTarget.performed += FollowMouse;

        Shooting = playerControls.Player.Fire;
        Shooting.Enable();
        Shooting.performed += Shoot;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        MovingTarget.Disable();
        MovingTarget.performed -= FollowMouse;
        Shooting.Disable();
    }

    private void FollowMouse(InputAction.CallbackContext context)
    {
        //Recieve the input value
        Target = CameraMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(Target.x, Target.y, transform.position.z);
    }

    #region Player_commands



    public void Shoot(InputAction.CallbackContext context)
    {

        //Instantiate the missle from the nearest base
        int index = 0;
        float baseDistance = 20;
        bool misslesEmpty = true;

        for (int i = 0; i < GS.buildingsManager.Bases.Length; i++)
        {
            if (GS.buildingsManager.Bases[i].CanShoot == true)
            {
                misslesEmpty = false;
                //Qual distância é a menor
                if (Vector2.Distance(transform.position, GS.buildingsManager.Bases[i].transform.position) < baseDistance)
                {
                    baseDistance = Vector2.Distance(transform.position, GS.buildingsManager.Bases[i].transform.position);
                    index = i;
                }
            }
        }

        Base nearestBase = GS.buildingsManager.Bases[index];
        
        if(misslesEmpty == false)
        SpawnMissle(nearestBase);

    }
    void SpawnMissle(Base nearestBase)
    {
        nearestBase.DecreaseMissle(1);

        GameObject missleTarget = Instantiate(MissleTarget);
        missleTarget.transform.position = new Vector2(Target.x, Target.y);
        Destroy(missleTarget, 10f);

        Missle missle = Instantiate(GS.allyMissle, nearestBase.transform);
        missle.target = missleTarget.transform;
    }
    #endregion
}
