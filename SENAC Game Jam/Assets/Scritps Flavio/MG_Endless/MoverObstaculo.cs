using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObstaculo : MonoBehaviour {
    [SerializeField] private float speed = 5f, limite = 5f;
    private Despawn despawn;

    private void Start()
    {
        despawn = GetComponent<Despawn>();
    }

    void Update() {
        transform.Translate(speed * Time.deltaTime * Vector2.left);

        if(transform.position.x < -limite && despawn != null)
            despawn.enabled = true;
    }

}
