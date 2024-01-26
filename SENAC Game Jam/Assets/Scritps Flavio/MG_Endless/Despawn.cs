using UnityEngine;

public class Despawn : MonoBehaviour {
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }
}
