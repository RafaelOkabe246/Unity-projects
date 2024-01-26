using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipHoldButton : MonoBehaviour
{
    [SerializeField]
    private Image skipFillImg;
    [SerializeField]
    private GameObject skipBtn;

    private float currentFill;
    [SerializeField]
    private float fillSpeed;

    private bool skipped;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            skipBtn.SetActive(true);
            skipFillImg.gameObject.SetActive(true);

            currentFill += fillSpeed * Time.deltaTime;

            if (currentFill >= 1)
                currentFill = 1f;

            if (skipFillImg.fillAmount >= 1)
            {
                SkipCutscene();
                skipped = true;
            }
        }
        else 
        {
            if (skipped)
                return;

            currentFill -= (fillSpeed / 2f) * Time.deltaTime;
            if (currentFill <= 0) 
            {
                currentFill = 0f;
                skipBtn.SetActive(false);
                skipFillImg.gameObject.SetActive(true);
            }
        }

        if (!skipFillImg.gameObject.active)
            return;

        skipFillImg.color = new Vector4(255f,
            1f * (1f - skipFillImg.fillAmount),
            1f * (1f - skipFillImg.fillAmount),
            255f);

        skipFillImg.fillAmount = currentFill;
    }

    private void SkipCutscene()
    {
        SoundManager.instance.PlayAudio(AudiosReference.buttonClick, AudioType.UI, null);

        SceneLoader.instance.LoadNextLevel();
    }
}
