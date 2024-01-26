using UnityEngine;

public class BinaryObject : MonoBehaviour, IPooledObject
{
    private float speed;

    public void OnObjectSpawn()
    {
        speed = Random.Range(0.5f, 3.5f) * Time.deltaTime;
    }

    private void Update()
    {
        transform.position += new Vector3(0f, -speed, 0f);
    }
}
