using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _hightlight;
    public Vector2 gridPosition;
    public Piece currentPiece;

    public void Init(bool isOffset, Vector2 _gridPosition)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        gridPosition = _gridPosition;
    }

    public void Interact()
    {

    }

    public void SetHighlight(bool i)
    {
        _hightlight.SetActive(i);
    }
}
