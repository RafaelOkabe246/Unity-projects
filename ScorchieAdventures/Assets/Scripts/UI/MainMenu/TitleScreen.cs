using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public ActivatableUI mainMenu;
    public Animator mainMenuTransitionAnim;
    public Animator titleScreenAnim;
    private bool canReceiveInput = true;
    public GameObject pressAnyKeyGO;

    [Space(5)]
    [Header("Save System")]
    public int numberOfGameSlots = 3;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && canReceiveInput) 
        {
            InitializeSaveSystem();

            SoundsManager.instance.PlayAudio(AudiosReference.buttonClicked, AudioType.BUTTON, null);

            StartCoroutine(StartMainMenu());
        }
    }

    private void InitializeSaveSystem() 
    {
        SaveSystem.GenerateGameSlots(numberOfGameSlots);
        StagesData.LoadStagesData(numberOfGameSlots);
    }

    private IEnumerator StartMainMenu() 
    {
        mainMenuTransitionAnim.SetTrigger("OpenMainMenu");
        titleScreenAnim.SetTrigger("OpenMainMenu");
        canReceiveInput = false;
        pressAnyKeyGO.SetActive(false);

        yield return new WaitForSeconds(2f);

        ScreenStack.instance.AddScreenOntoStack(mainMenu);
        gameObject.SetActive(false);
    }
}
