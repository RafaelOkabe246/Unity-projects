using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    Occupied,
    Empty
}

/// <summary>
/// Tile script
/// </summary>
public class BattleTile : MonoBehaviour
{
    [Header("Pathdind")]
    public List<BattleTile> neighbourTiles = new List<BattleTile>();
    public List<BattleTile> retreatTiles = new List<BattleTile>();
    [HideInInspector]
    public float fcost, gcost, hcost;
    public BattleTile cameFrom;

    [SerializeField]
    private TileState currentState;

    public Vector2 gridPosition;
    public Grid levelGrid;

    [Header("Objects interactions")]
    [SerializeField]
    private ObjectTile occupiedObject; //Current object occupiyng the tile
    public ObjectTile onMoveObject; //Curren object that will move to the tile
    public ObjectTile targetingObject; //Current object targeting the tile
    public TriggerEventObject interactableObject; //Current interactable object in the tile

    private delegate void TileStateChanged(TileState newState, ObjectTile occupiedObject);
    private event TileStateChanged OnTileStateChanged;

    [Space(5)]
    [Header("Properties")]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer gfx;
    private int sortingLayerBase;

    [Space(5)]
    [Header("Colors")]
    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    private void OnEnable()
    {
        OnTileStateChanged += SetNewTileState;
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        OnTileStateChanged -= SetNewTileState;
    }

    public void SetSortingLayerBase(int baseLayerNumber)
    {
        sortingLayerBase = baseLayerNumber;
    }

    public void CalculateFCost()
    {
        fcost = gcost + hcost;
    }

    public void AddInteractableObject(TriggerEventObject _interactableObject)
    {
        interactableObject = _interactableObject;
    }

    public void ClearInteractableObject()
    {
        interactableObject = null;
    }

    public void OccupyTile(ObjectTile occupier)
    {
        OnTileStateChanged?.Invoke(TileState.Occupied, occupier);
    }

    public void ChooseColor(int color) 
    {
        //Choose between 0 and 1 in order to select the color
        gfx.color = (color == 0) ? color1 : color2;
    }

    

    private void SetNewTileState(TileState newTileState, ObjectTile _occupiedObject)
    {
        currentState = newTileState;
        if (newTileState != TileState.Empty)
        {
            occupiedObject = _occupiedObject;
            SetOccupyingObjectSortingLayer();
        }
        else
            occupiedObject = null;
        anim.SetBool("Empty", newTileState == TileState.Empty);
        anim.SetBool("Occupied", newTileState == TileState.Occupied);

        CheckInteractableObject();
    }

    void CheckInteractableObject()
    {
        if (interactableObject == null)
            return;

        if(currentState == TileState.Occupied && occupiedObject.gameObject.CompareTag("Player"))
            interactableObject.SetCountdownInteractableEvent(true);
        else if(currentState == TileState.Empty)
            interactableObject.SetCountdownInteractableEvent(false);
    }

    public void ClearTile()
    {
        OnTileStateChanged?.Invoke(TileState.Empty, null);
    }

    public TileState GetCurrentState()
    {
        return currentState;
    }

    public ObjectTile GetOnMoveObject()
    {
        return onMoveObject;
    }

    public ObjectTile GetInteractableObject()
    {
        return interactableObject;
    }

    public ObjectTile GetOccupyingObject() 
    {
        return occupiedObject;
    }

    public bool CompareStateWith(TileState tileStateToCompare) 
    {
        return currentState == tileStateToCompare;
    }

    public void SetOccupyingObjectSortingLayer() 
    {
        SpriteRenderer gfx =  occupiedObject.GetComponentInChildren<SpriteRenderer>();
        gfx.sortingOrder = (int)(sortingLayerBase - gridPosition.y);
    }

    private void OnDrawGizmos()
    {
        if(CompareStateWith(TileState.Occupied))
            Gizmos.DrawWireSphere(transform.position,0.5f);
    }
}
