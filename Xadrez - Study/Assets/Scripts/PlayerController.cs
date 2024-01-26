using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class PlayerController : MonoBehaviour
{
    PiecesManager piecesManager;

    public PlayerControls inputActions;

    public List<Piece> _pieces;


    private void Awake()
    {
        inputActions = new PlayerControls();
    }


    private void Start()
    {
        piecesManager = new PiecesManager();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Actions.TouchPoint.performed += _context => CheckTouchPos(_context.ReadValue<Vector2>());
        //inputActions.Actions.ClickPoint.performed += _context => CheckTouchPos(_context.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Actions.TouchPoint.performed -= _context => CheckTouchPos(_context.ReadValue<Vector2>());
        //inputActions.Actions.ClickPoint.performed -= _context => CheckTouchPos(_context.ReadValue<Vector2>());
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckTouchPos(Mouse.current.position.ReadValue());
        }
    }

    void CheckTouchPos(Vector2 touchPos)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPos), Vector2.zero);


        if (hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactableObject))
        {
            //The hit object is a piece
            //Highlight possible moves
            interactableObject.Interact();

        }

        //Debug.Log($"Touch World Position: {touchPos}");
        //Debug.Log($"Object: {hitInfo.collider.gameObject}");
    }

}
