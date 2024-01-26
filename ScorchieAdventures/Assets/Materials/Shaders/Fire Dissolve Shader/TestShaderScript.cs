using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderScript : MonoBehaviour
{
    [SerializeField] private Material material;
    private float dissolveAmount;
    private bool isDissolving;

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isDissolving = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDissolving = false;
        }
    }
}
