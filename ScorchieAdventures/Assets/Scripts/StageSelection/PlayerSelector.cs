using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public StagePoint currentStagePoint;
    private StagePoint targetStagePoint;

    public float standardMoveSpeed;
    public float fasterMoveSpeed;
    private float moveSpeed = 5f;

    private Vector2 moveInput;
    private Animator anim;
    public Animator cameraAnimator;
    public Transform cameraTrans;

    private bool movingToDestination;
    private bool canMoveOrSelect = true;

    #region DELEGATES
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        if (currentStagePoint == null)
            currentStagePoint = GameObject.Find("StagePoint").GetComponent<StagePoint>();
        currentStagePoint.stageWorldUI.CallEnableAnimation();
        //currentStagePoint.stageWorldUI.ForceEnableAnimation();

        StartCoroutine(BindDelegateWithDelay());
    }

    private IEnumerator BindDelegateWithDelay() 
    {
        yield return new WaitForSeconds(0.1f);
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    #endregion

    private void Update()
    {
        if (!movingToDestination && canMoveOrSelect) 
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");

            if (moveInput.x > 0 && currentStagePoint.nextStagePoint != null && currentStagePoint.nextStagePoint.stageState != StageState.NOT_VISIBLE)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                SetDestination(currentStagePoint.nextStagePoint);
            }
            else if (moveInput.x < 0 && currentStagePoint.previousStagePoint != null && currentStagePoint.previousStagePoint.stageState != StageState.NOT_VISIBLE) 
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                SetDestination(currentStagePoint.previousStagePoint);
            }

            if (Input.GetButtonDown("Submit") && currentStagePoint != null) 
            {
                currentStagePoint.GetComponent<StagePoint>().SelectStage();
            }
        }

        if (movingToDestination) 
        {
            if (Input.GetButton("Submit"))
            {
                moveSpeed = fasterMoveSpeed;
                anim.speed = 1.5f;
            } 
            else if (Input.GetButtonUp("Submit")) 
            {
                moveSpeed = standardMoveSpeed;
                anim.speed = 1f;
            }
        }
    }

    private void SetDestination(StagePoint destination) 
    {
        targetStagePoint = destination;
        movingToDestination = true;
        anim.SetBool("isMoving", movingToDestination);
        canMoveOrSelect = false;
        cameraAnimator.SetTrigger("ZoomIn");
        currentStagePoint.stageWorldUI.CallDisableAnimation();
        StartCoroutine(MoveToDestination(destination.transform));
    }

    private IEnumerator MoveToDestination(Transform targetPosition) 
    {
        yield return new WaitForSeconds(1 / 60);
        while (transform.position != targetPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
            cameraTrans.position = new Vector3(transform.position.x, cameraTrans.position.y, cameraTrans.position.z);
            yield return null;
        }

        Debug.Log("Reached the Destination!");
        currentStagePoint = targetStagePoint;
        targetStagePoint = null;
        movingToDestination = false;
        anim.SetBool("isMoving", movingToDestination);
        anim.speed = 1f;
        canMoveOrSelect = true;

        moveSpeed = standardMoveSpeed;

        currentStagePoint.stageWorldUI.CallEnableAnimation();

        cameraAnimator.SetTrigger("ZoomOut");
    }

    public void OnStageSelected(bool selected) 
    {
        canMoveOrSelect = selected;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        canMoveOrSelect = (newGameState == GameState.Gameplay);
    }
}
