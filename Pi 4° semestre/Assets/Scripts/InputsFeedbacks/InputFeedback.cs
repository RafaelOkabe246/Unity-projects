using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFeedback : MonoBehaviour, IPooledObject
{
    [Header("Properties")]
    [SerializeField]
    private TextMeshProUGUI inputText;
    private Image inputImage;

    [Header("Animation")]
    private float animationSpeed;
    private float animationTimeCount;
    private float animationTimeLimit;
    private int animationIndex = 0;
    private Sprite currentSpr;
    private Sprite[] spritesArray;

    public void InitializeFeedback(string strInputText, Sprite[] sprites, float animSpeed) 
    {
        inputText.text = strInputText;
        spritesArray = sprites;
        animationSpeed = animSpeed;
    }

    public void OnObjectSpawn()
    {
        
    }

    private void Update()
    {
        animationTimeCount += animationSpeed * Time.deltaTime;

        if (animationTimeCount >= animationTimeLimit)
        {
            animationTimeCount = 0f;
            AnimateInput();
        }
    }

    private void AnimateInput()
    {
        animationIndex++;
        if (animationIndex >= spritesArray.Length)
            animationIndex = 0;

        currentSpr = spritesArray[animationIndex];

        inputImage.sprite = currentSpr;
    }
}
