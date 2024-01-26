using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider : MonoBehaviour
{
    [SerializeField] internal Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Item))
        {
            var item = other.GetComponent<Item>();
            if (item)
            {
                _character._Inventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag(Tags.LeftBorder))
        {
            _character.gameObject.transform.position = _character.RightSpawn.transform.position;
        }

        if (other.gameObject.CompareTag(Tags.RightBorder))
        {
            _character.gameObject.transform.position = _character.LeftSpawn.transform.position;
        }
    }
}
