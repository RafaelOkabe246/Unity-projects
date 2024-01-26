using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosReference : MonoBehaviour
{
    [Header("Ui audios")]
    public const string buttonHover = "MoveUI";
    public const string buttonHover2 = "MoveUI2";
    public const string buttonClick = "Click";

    public const string textFillTypping = "textFillTypping";


    [Header("Characters audios")]
    public const string characterMove = "Move";
    public const string playerTakeDamage = "playerTakeDamage";

    public const string robotTakeKnockdown = "knockdown";
    public const string robotTakeDamage = "robotTakeDamage";
    public const string robotDestroyed = "robotDestroyed";

    public const string lightAttack = "lightAttack";
    public const string missAttack = "missAttack";
    public const string strongAttack = "strongAttack";


    [Header("Shop audios")]
    public const string buyItem = "buyItem";
    public const string failBuyItem = "failBuyItem";

    [Header("Objects audios")]
    public const string objectTakeDamage = "objectTakeDamage";
    public const string objectDestroyed = "objectDestroyed";


    [Header("Musics")]
    public const string mainMenuMusic = "mainMenuMusic";
    public const string gameplayMusic = "gameplayMusic";
    public const string bossLevelMusic = "bossMusic";
    public const string cafeteriaMusic = "cafeteriaMusic";
}