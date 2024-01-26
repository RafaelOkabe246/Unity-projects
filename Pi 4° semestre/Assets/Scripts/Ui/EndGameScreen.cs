using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScreen : ActivatableUI
{
    [Space(10)]
    [Header("End Game Screen")]

    private EndGameInfo endGameInfo;

    public TextMeshProUGUI enemiesDefeated;
    public Animator enemiesAnim;
    public TextMeshProUGUI obstaclesDestroyed;
    public Animator obstaclesAnim;
    public TextMeshProUGUI levelsPassed;
    public Animator levelsAnim;

    protected override void OnEnable()
    {
        base.OnEnable();

        endGameInfo = GetComponent<EndGameInfo>();
        FillInformations();
    }

    private void FillInformations() 
    {
        enemiesDefeated.text = "x 0";
        int enDefeated = ProgressionManager.instance.currentRunInfo.enemiesDestroyed;
        endGameInfo.FillInfo(enemiesDefeated, enDefeated, enemiesAnim);

        if (!obstaclesDestroyed)
            return;

        obstaclesDestroyed.text = "x 0";
        int obsDestroyed = ProgressionManager.instance.currentRunInfo.objectsDestroyed;
        endGameInfo.FillInfo(obstaclesDestroyed, obsDestroyed, obstaclesAnim);

        if (!levelsPassed)
            return;

        levelsPassed.text = "x 0";
        int lvlPassed = ProgressionManager.instance.currentRunInfo.currentStageIndex;
        endGameInfo.FillInfo(levelsPassed, lvlPassed, levelsAnim);
    }
}
