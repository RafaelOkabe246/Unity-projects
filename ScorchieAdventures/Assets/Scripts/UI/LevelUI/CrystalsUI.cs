using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrystalsUI : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] private Animator anim;

    [Header("Informations")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI quantityTextShadow;

    public void UpdateUIInformations(int collectedFruits)
    {
        string textToAdd = "";

        if (collectedFruits < 10)
            textToAdd += "0";

        textToAdd += collectedFruits.ToString();

        quantityText.text = textToAdd;
        quantityTextShadow.text = quantityText.text;
    }

    public Vector3 GetCrystalsUIworldPosition()
    {
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.transform.position, Camera.main, out worldPosition);
        return worldPosition;
    }

    public void CallCollectAnimationInUI()
    {
        anim.SetTrigger("Collect");
    }

    public void CallShowAnimationInUI() 
    {
        anim.SetTrigger("Show");
    }
}
