using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuAi : MonoBehaviour
{
    public enum CpuBehaviourMode
    {
        Chase,
        Defensive
    }
    //always chase mode for the sake of time
    public CpuBehaviourMode currentMode;

    public Unit _unit;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }


    public void AttackNearestPlayerUnit()
    {
        int index;
        float baseDistance = 100f;
        for(int i = 0; i < UnitManager.instance._playerUnits.Count; i++)
        {
            if (Vector2.Distance(transform.position, UnitManager.instance._playerUnits[i].transform.position) < baseDistance)
            {
                baseDistance = Vector2.Distance(transform.position, UnitManager.instance._playerUnits[i].transform.position);
                index = i;
            }
        }

    }

}
