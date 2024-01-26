using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_scenario_change : MonoBehaviour
{
    public GameObject Normal_Grid;
    public GameObject New_Grid;


    public GameObject Self;

    private void Awake()
    {
        New_Grid.SetActive(false);
    }

    public void Changing_scenario()
    {
        Normal_Grid.SetActive(false);
        New_Grid.SetActive(true);

    }
}
