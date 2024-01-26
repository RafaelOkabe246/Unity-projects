using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosReference : MonoBehaviour
{
    [Header("Enemies sounds")]
    public const string enemyHit = "enemyHit";

    [Header("Player sounds")]
    public const string land0 = "Land0";
    public const string land1 = "Land1";
    public const string playerLandingRocky = "playerLandingRocky";
    public const string dashStart = "DashStart";
    public const string dashFlame = "DashFlame";
    public const string dashHitEnemy = "DashHitEnemy";
    public const string playerDeath = "playerDeath";
    public const string jump0 = "Jump0";
    public const string jump1 = "Jump1";
    public const string jump2 = "Jump2";
    public const string jump3 = "Jump3";
    public const string jump4 = "Jump4";
    public const string footstep = "Footstep";

    [Header("Colectable sounds")]
    public const string fruitCollect = "fruitCollect";
    public const string crystalCollect = "crystalCollect";


    [Header("Button Gimmick")]

    [Header("Torch")]
    public const string torchOn = "torchOn";
    public const string torchIdle = "torchIdle"; //plays when the player is close enough

    [Header("Burning floor")]
    public const string floorBurning = "floorBurning";
    public const string floorRegenerate = "floorRegenerate";


    [Header("Vertical plataform")]
    public const string startFloating = "startFloating";
    public const string startFalling = "startFalling";


    [Header("Ui buttons")]
    public const string buttonHoover = "buttonHoover";
    public const string buttonClicked = "buttonClicked";

    [Header("Victory screen")]
    public const string stageCompleted = "stageCompleted";

    [Header("Title screen")]
    public const string startGame = "startGame";
    public const string selectedSaveSlot = "selectedSaveSlot";
    public const string deletSlot = "deletSlot";
    public const string settingPanelTransition = "settingPanelTransition"; //in this case, we can use the same sound as the Open Pause sound.

    [Header("Pause screen")]
    public const string openPause = "openPause";
    public const string exitPause = "exitPause";

}
