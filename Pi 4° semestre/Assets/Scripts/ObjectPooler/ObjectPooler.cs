using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region SINGLETON

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(gameObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void SpawnFromPoolAndAddParent(string tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject objectToSpawn = SpawnFromPool(tag, position, rotation);
        if (objectToSpawn != null)
            objectToSpawn.transform.SetParent(parent);
    }

    public void SpawnAfterImageFromPool(string tag, Vector3 position, Transform transformGO, Quaternion rotation, Sprite objectSprite, bool flipX) 
    {
        GameObject objectToSpawn = SpawnFromPool(tag, position, rotation);
        if (objectToSpawn != null)
            objectToSpawn.GetComponent<AfterImage>().SetAfterImageSprite(objectSprite, flipX, transformGO);
    }

    public void SpawnInputFeedbackFromPool(Transform parent, Quaternion rotation, KeyCode input, string inputText, float animationSpeed) 
    {
        GameObject objectToSpawn = SpawnFromPool("InputFeedback", Vector2.zero, rotation);
        if (objectToSpawn != null)
            objectToSpawn.transform.SetParent(parent);

        InputFeedback inputFeedback = objectToSpawn.GetComponent<InputFeedback>();

        if (!inputFeedback) 
        {
            Debug.LogError("Input Feedback: the object that you want to spawn from pool is not of type Input Feedback," +
                " check if the tag you're using the Object Pooler instance is correctly applied on the inspector");
            return;
        }

        Sprite[] sprs = InputSpriteFinder.instance.FindInputSpritesFromKeyCode(input);

        inputFeedback.InitializeFeedback(inputText, sprs, animationSpeed);
    }
}
