using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageWorldUI : MonoBehaviour
{
    private Animator anim;

    public TextMeshProUGUI recordTime;
    public TextMeshProUGUI fruitsQuantity;
    public TextMeshProUGUI crystalQuantity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FillInformations(string time, string fruits, string maxFruits, string crystals, string maxCrystals) 
    {
        recordTime.text = time;
        fruitsQuantity.text = fruits + "/" + maxFruits;
        crystalQuantity.text = crystals + "/" + maxCrystals;
    }

    public void CallEnableAnimation() 
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetTrigger("Open");
    }

    public void ForceEnableAnimation() 
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.Play("ANIM_OpenedStageWorldUI");
    }

    public void CallDisableAnimation() 
    {
        anim.SetTrigger("Close");
    }
}
