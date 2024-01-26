using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolVFXBase : MonoBehaviour
{
    public float delayToDeactivate = 0.5f;

    public void OnObjectSpawn()
    {
        StartCoroutine(DeactivateObject());
    }

    private IEnumerator DeactivateObject()
    {
        yield return new WaitForSeconds(delayToDeactivate);

        gameObject.SetActive(false);
    }
}
