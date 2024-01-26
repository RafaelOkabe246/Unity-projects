using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameInfo : MonoBehaviour
{
    [SerializeField]
    protected float typingDelay;

    public void FillInfo(TextMeshProUGUI textToFill, int infoToAdd, Animator anim) 
    {
        StartCoroutine(FillCoroutine(textToFill, infoToAdd, anim));
    }

    protected IEnumerator FillCoroutine(TextMeshProUGUI textToFill, int infoToAdd, Animator anim)
    {
        for (int i = 0; i <= infoToAdd; i++)
        {
            SoundManager.instance.PlayAudio(AudiosReference.textFillTypping, AudioType.UI, null);
            yield return new WaitForSeconds(typingDelay);
            anim.SetTrigger("Toggle");
            textToFill.text = "x " + i;
        }
    }
}
