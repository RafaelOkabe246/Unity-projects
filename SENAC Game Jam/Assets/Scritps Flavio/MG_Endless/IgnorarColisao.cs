using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorarColisao : MonoBehaviour {
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10);
    }
}
