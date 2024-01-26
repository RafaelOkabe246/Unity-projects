using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] internal PlayerInputs inputs;
    [SerializeField] internal CharacterMovement _movement;
    [SerializeField] internal CharacterCollider characterCollider;
    [SerializeField] internal Rigidbody2D rig;
    [SerializeField] internal Collider2D _collider;

    //Inventory
    public Inventory _Inventory;

    //Movement atributes
    public float speed = 3f;
    public Vector2 direction;
    public bool isGrounded;

    //Spawn atributes
    public Transform RightSpawn;
    public Transform LeftSpawn;

    //Animator atributes
    [SerializeField] internal Animator _animator;

    void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
        _animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        _movement = GetComponent<CharacterMovement>();
        _collider = GetComponent<Collider2D>();
        characterCollider = this.transform.GetChild(1).GetComponent<CharacterCollider>();
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<ItemObject>();
        if (item)
        {
            //_Inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    */
}
