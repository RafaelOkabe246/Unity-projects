using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActPoints : MonoBehaviour
{
    public Image[] actOrbsUI; //orbs in the UI

    [SerializeField] private int maxActOrbs = 5;
    private BattleSystem bs;

    void Start()
    {
        bs = GetComponent<BattleSystem>();
        bs.actOrbs = maxActOrbs;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < actOrbsUI.Length; i++)
        {
            if (i < bs.actOrbs)
            {
                actOrbsUI[i].enabled = true;
            }
            else
            {
                actOrbsUI[i].enabled = false;
            }
        }
    }

    public void ResetActOrbs()
    {
        //Setting the number of orbs equal to the initial number
        bs.actOrbs = maxActOrbs;
    }
}
