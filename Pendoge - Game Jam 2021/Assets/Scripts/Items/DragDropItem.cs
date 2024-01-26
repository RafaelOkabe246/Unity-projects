using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropItem : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    public InventorySlot inventorySlot;
    private RectTransform rectTransform;

    [SerializeField]
    private PlayerControl playerControl;

    public static Tripulante SelectedTripulante;

    private void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerControl>();
        inventorySlot = GetComponentInParent<InventorySlot>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition; //Input.mousePosition;
        /*
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Tripulante")
        {
            playerControl.UseItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<Tripulante>(), inventorySlot.ReferenceItem, inventorySlot);
        }
        */

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Tripulante")
        {
            //Tripulante tripulante = eventData.pointerCurrentRaycast.gameObject.GetComponent<Tripulante>();

               playerControl.UseItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<Tripulante>(), inventorySlot.ReferenceItem, inventorySlot);
        }
    }
}
