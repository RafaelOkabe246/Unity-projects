using UnityEngine;

public class BinarySpawner : MonoBehaviour
{
	ObjectPooler objectPooler;

	[Header("SpawnProperties")]
	[SerializeField] private float timeToSpawn;
	private float timeToSpawnCount;
	[SerializeField] private float minPosX;
	[SerializeField] private float maxPosX;

	private void Start()
	{
		objectPooler = ObjectPooler.Instance;
	}

    private void Update()
    {
		timeToSpawnCount += Time.deltaTime;
		if (timeToSpawnCount >= timeToSpawn)
			SpawnBinary();
    }

	private void SpawnBinary() 
	{
		timeToSpawnCount = 0;

		Vector3 position = new Vector3(Random.Range(minPosX, maxPosX), transform.position.y, transform.position.z);
		objectPooler.SpawnFromPool("Binary", position, Quaternion.identity);
	}
}
