using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosManager : MonoBehaviour
{
    public CutsceneController cutsceneController;
    public SlotManager slotManager;
    public int maxResult;
    public int minResult;

    public int dadosResult;


    public void PlayDados()
    {
        SlotManager.turnosGansoEspera += 1;
        PlayPataDado();
        if (SlotManager.turnosGansoEspera == slotManager.turnoGansoPadreSair && !SlotManager.hasGansoExit)
        {
            SlotManager.hasGansoExit = true;
            cutsceneController.SetCutscene(cutsceneController.gansoTriggerCutscene);
        }


        if(SlotManager.turnosGansoEspera > slotManager.turnoGansoPadreSair)
            PlayGansoDado();
    }

    public void PlayPataDado()
    {
        slotManager.MovePataToNewSlot(randomResult());
    }

    public void PlayGansoDado()
    {
        slotManager.MoveGansoToNewSlot(1);
    }

    public int randomResult()
    {
        return Random.Range(minResult, maxResult + 1);
    }
}
