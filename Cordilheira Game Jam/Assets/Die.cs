using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SceneController.ResetLevel();
    }
}
