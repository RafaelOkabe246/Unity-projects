using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSprite : MonoBehaviour
{
    public KeyCode keyCode;
    public KeyCode gamepadCode;
    public Image spr;
    private Sprite[] sprites;

    [SerializeField] private float animationSpeed = 5f;
    private float animationTimeCount;
    private float animationTimeLimit = 10f;
    private int animationIndex = 0;

    private bool isUsingGamepad;
    private RectTransform rectTrans;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(36, 36);

        string[] joystickNames = Input.GetJoystickNames();

        Debug.Log(gameObject.name);

        if (joystickNames.Length > 0)
        {
            Debug.Log("Player is using a gamepad.");
            InputController.instance.inputsDictionary.TryGetValue(gamepadCode, out sprites);
            Debug.Log(sprites.Length);
            isUsingGamepad = true;

        }
        else if (joystickNames.Length <= 0)
        {
            isUsingGamepad = false;
            Debug.Log("No gamepad detected.");
            InputController.instance.inputsDictionary.TryGetValue(keyCode, out sprites);

            if (!isUsingGamepad &&
                (keyCode == KeyCode.Space || keyCode == KeyCode.Backspace ||
                keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift)
                || keyCode == KeyCode.Delete)
            {
                rectTrans.sizeDelta = new Vector2(66, 36);
            }

        }

        animationIndex = 0;

        if (sprites != null) 
        {
            spr.sprite = sprites[0];
            animationIndex = 0;
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

    private void LateUpdate()
    {
        DetectGamepad();
    }

    private void AnimateInput() 
    {
        animationIndex++;
        if (animationIndex >= sprites.Length)
            animationIndex = 0;

        spr.sprite = sprites[animationIndex];
    }

    private void DetectGamepad() 
    {
        string[] joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length > 0 && !isUsingGamepad)
        {
            Debug.Log("Player is using a gamepad.");
            InputController.instance.inputsDictionary.TryGetValue(gamepadCode, out sprites);
            isUsingGamepad = true;
            animationIndex = 0;
            rectTrans.sizeDelta = new Vector2(36, 36);
        }
        else if(joystickNames.Length <= 0 && isUsingGamepad)
        {
            isUsingGamepad = false;
            Debug.Log("No gamepad detected.");
            InputController.instance.inputsDictionary.TryGetValue(keyCode, out sprites);
            isUsingGamepad = false;
            animationIndex = 0;

            if (!isUsingGamepad &&
            (keyCode == KeyCode.Space || keyCode == KeyCode.Backspace ||
            keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift || 
            keyCode == KeyCode.Delete))
            {
                rectTrans.sizeDelta = new Vector2(66, 36);
            }
            else
                rectTrans.sizeDelta = new Vector2(36, 36);
        }
    }
}
