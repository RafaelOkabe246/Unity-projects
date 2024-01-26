using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] internal Player _character;

    private void Awake()
    {
        _character = GetComponent<Player>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Enemy))
        {
            _character.TakeDamage(other.GetComponent<Enemy>().AttackDamage);
        }

        if (other.gameObject.CompareTag("Final_point"))
        {
            SceneManager.LoadScene("End scene");
        }

        if (other.gameObject.CompareTag("Death"))
        {
            transform.position = _character.Checkpoint.transform.position;
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            _character.Checkpoint = other.transform;
        }
    }
    
}
