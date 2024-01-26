using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUIFeedback : MonoBehaviour
{
    public KeyCode keyCode;
    public Image spr;
    private Sprite[] sprites;

    [SerializeField] private float animationSpeed = 5f;
    private float animationTimeCount;
    private float animationTimeLimit = 10f;
    private int animationIndex = 0;

    private void Start()
    {
        InputSpriteFinder.instance.inputsDictionary.TryGetValue(keyCode, out sprites);

        if (sprites != null)
        {
            spr.sprite = sprites[0];
        }

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
        if (animationIndex >= sprites.Length)
            animationIndex = 0;

        spr.sprite = sprites[animationIndex];
    }
}
