using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_manager : MonoBehaviour
{
    public bool Has_Checkpoint;
    public Vector2 LastCheckpointPos;

    public static Checkpoint_manager instance;


    void Start()
    {
        
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        
    }

    private void Update()
    {
        if (LastCheckpointPos == new Vector2(0,0))
        {
            Has_Checkpoint = false;
        }
        else
        {
            Has_Checkpoint = true;
        }
    }
}
