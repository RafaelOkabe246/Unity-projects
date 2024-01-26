using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public DadosManager dadosManager;

    [Header("Slots")]
    public List<Slot> slots;
    //public Slot currentPataSlot;
    public static int currentPataSlotIndex;

    //public Slot currentGansoSlot;
    public static int currentGansoSlotIndex;

    [Space(10)]
    [Header("Pata e Ganso ")]
    public Pata pata;
    public Pata gansoPadre;
    public static int turnosGansoEspera;
    public int turnoGansoPadreSair;
    public static bool hasGansoExit;

    private void Start()
    {
        SetPataStartPosition();
        if(hasGansoExit)
            SetGansoStartPosition();
    }

    void SetPataStartPosition()
    {
        pata.gameObject.transform.position = slots[currentPataSlotIndex].gameObject.transform.position;
        //currentPataSlot = slots[currentPataSlotIndex];
        //currentGansoSlot = slots[currentGansoSlotIndex];
    }

    public void SetGansoStartPosition()
    {
        gansoPadre.gameObject.transform.position = slots[currentGansoSlotIndex].gameObject.transform.position;
    }


    private void OnEnable()
    {
        CheckLastMinijogoResult();
    }

    void CheckLastMinijogoResult()
    {
        if (!GameManager.instance.hasWonMinigame && turnosGansoEspera > turnoGansoPadreSair)
        {
            Debug.Log("Moveu ganso extra");
            StartCoroutine(GansoExtraMovement());
        }
    }

    IEnumerator GansoExtraMovement()
    {
        UiManager.instance.dadosButton.SetActive(false);
        yield return new WaitForSeconds(4f);
        dadosManager.PlayGansoDado();
        UiManager.instance.dadosButton.SetActive(true);
        yield return null;
    }


    public void MovePataToNewSlot(int moveSteps)
    {
        Debug.Log(moveSteps);
        UiManager.instance.ShowResultadoDosDados(moveSteps);

        int newSlotIndex = currentPataSlotIndex;
        if(GameManager.instance.hasWonMinigame)
            newSlotIndex += moveSteps;

        if (newSlotIndex > slots.Count  || newSlotIndex == slots.Count )
        {
            Debug.Log("Ganhou o jogo");

            StartCoroutine(pata.Move(slots[slots.Count -1].gameObject.transform));
            SceneManagement.instance.LoadLevel(SceneManagement.mainMenuSceneIndex);
            return;
        }
        else
        {
            currentPataSlotIndex = newSlotIndex;
        }
        //currentPataSlot = slots[currentPataSlotIndex];
        if(turnosGansoEspera > turnoGansoPadreSair)
            CheckGansoPataPosition();

        StartCoroutine(pata.Move(slots[currentPataSlotIndex].gameObject.transform));

        UiManager.instance.Invoke("ShowMinigamePreview", 4f);

    }



    public void MoveGansoToNewSlot(int moveSteps)
    {
        int newSlotIndex = currentGansoSlotIndex + moveSteps;

        currentGansoSlotIndex = newSlotIndex;

        //currentGansoSlot = slots[currentGansoSlotIndex];
        if (turnosGansoEspera > turnoGansoPadreSair)
            CheckGansoPataPosition();

        StartCoroutine(gansoPadre.Move(slots[currentGansoSlotIndex].gameObject.transform));
    }


    void CheckGansoPataPosition()
    {
        if(currentGansoSlotIndex == currentPataSlotIndex)
        {
            Debug.Log("Voce perdeu o jogo");
            SceneManagement.instance.LoadLevel(SceneManagement.gameOverSceneIndex);
        }
    }
}
