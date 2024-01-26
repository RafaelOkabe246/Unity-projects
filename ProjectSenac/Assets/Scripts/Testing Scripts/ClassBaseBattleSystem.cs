using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ClassBaseBattleState { BATTLE_START, PLAYER_TURN, PLAYER_TARGET_SELECT, PLAYER_ACT, ENEMY_TURN, ENEMY_ACT, BATTLE_END }

public class ClassBaseBattleSystem : MonoBehaviour
{
    public ClassBaseBattleState state;
    public int turn; //Player and enemy characters turn

    [Header("Players")]
    public Player[] _Players;
    public Transform[] PlayersPosition;
    public GameObject PlayersActUI;
    public List<Action> PlayerActions = new List<Action>();

    [Header("Enemies")]
    public Enemy[] _Enemies;
    public Transform[] EnemiesPosition;
    public List<Action> EnemyActions = new List<Action>();

    [Header("Orbs")]
    //public int actOrbs; //number of orbs
    //private PlayerActPoints playerActPoints;

    [Header("Enemy Wave System")]

    public int wave; //Battle number
    public int maxWaves; //Max number of battles
    public BattleWave[] battleWaves; //List of waves of enemies

    [Header("TargetSelect")]

    public ClassBaseMouseClick mouseClick; //get activated during the PLAYER_ACTION_SELECT state
    public ClassBaseSkillButton selectedButton; //the selected button with the selected skill

    void Start()
    {
        //Defining the number of waves in the battle
        maxWaves = battleWaves.Length;

        //Initializing battle
        state = ClassBaseBattleState.BATTLE_START;

        //Getting the PlayerActPoints component
        //playerActPoints = GetComponent<PlayerActPoints>();

        StartCoroutine(StartBattle());
    }

    void Update()
    {
        //Checking the current battle state
        switch (state)
        {
            case (ClassBaseBattleState.PLAYER_TURN):
                onPlayerTurn();
                break;
            case (ClassBaseBattleState.PLAYER_TARGET_SELECT):
                onTargetSelect();
                break;
            //Enemy battle state
            case (ClassBaseBattleState.ENEMY_TURN):
                onEnemyTurn();
                break;
            case (ClassBaseBattleState.ENEMY_ACT):
                
                break;
        }
    }

    IEnumerator StartBattle()
    {

        //Spawning Units
        //SpawnPlayerCharacters();
        SpawnEnemies();

        //Finding and positioning units
        FindPlayerUnits();
        FindEnemyUnits();
        SettingPlayerUnitsPosition();
        SettingEnemyUnitsPosition();

        //Setting the Mouse Click object as enabled
        mouseClick.enabled = false;

        yield return new WaitForSeconds(2f);

        //Starting the player's turn;
        state = ClassBaseBattleState.PLAYER_TURN;
        Debug.Log("Battle started, now it's the player's turn!");

    }

    void onTargetSelect()
    {
        mouseClick.gameObject.SetActive(true);
    }

    #region PLAYER_TURN

    public void onPlayerSelectAction()
    {
        Debug.Log("Action selected!");
        //Disabling the selected character's UI
        _Players[turn].DisablePlayerHUD();
        //Getting the next player character
        turn++;
        //if the value is greater than the number of players, then the acting turn occurs
        
        if (turn < _Players.Length)
        {
            //check if the unit is dead
            if (_Players[turn].IsDead == false)
            {
               onPlayerTurn();
            }
            else
            {
                Debug.Log(_Players[turn].CharStats.CharName + " is dead and can't act!");
                onPlayerSelectAction();
            }
        }
        else
        {
            //Starting the player acting turn
            turn = 0;
            state = ClassBaseBattleState.PLAYER_ACT;
            onPlayerAct();
        }
        
    }



    void onPlayerTurn()
    {
        //Enabling the player's UI
        PlayersActUI.SetActive(true);
        _Players[turn].EnablePlayerHUD();
    }

    #endregion


    #region Player_Act
    /*
    1. Check the list of actions
    2. Play the animation and the interaction according to the character and it is current action
    3. After finished, play the next on the list
    4. Change to enemy turn
    */
    void onPlayerAct()
    {
        //Reseting the Action Points
        //playerActPoints.ResetActOrbs();

        //Starting the enemy turn
        state = ClassBaseBattleState.ENEMY_TURN;
    }

    #endregion


    #region Enemy_Turn
    //1. Check how many enemies are in the list
    //2. Save their actions
    //3. In the last one, finish the turn

    void onEnemyTurn()
    {
        if (turn < _Enemies.Length)
        {
            foreach(Enemy enemy in _Enemies)
            {
                if(enemy.IsDead == false)
                {
                    //Do the logic that will send the action to the list
                }
            }
        }
        else
        {
            Debug.Log("Enemy turn finished. Start enemyAct");
            turn = 0;
            StartCoroutine(onEnemyAct());
        }
    }
    #endregion

    #region Enemy_Act
    //1. Play each action consecutively with the animations
    //2. If all the enemies did their actions, change to player's turn
    
    IEnumerator onEnemyAct()
    {
        yield return new WaitForSeconds(1); 
    }
    

    #endregion

    #region Battle_End
    /*
    IEnumerator OnBattleEnd()
    {

    }
    

    
    IEnumerator OnGameOver()
    {
        //End the battle and open the game over screen
        //yield return new WaitForSeconds(1f);

        //Open the game over screen
    }
    */
    #endregion

    #region Spawning Units

    void SpawnPlayers()
    {
        //Method under develpment
    }
    void SpawnEnemies()
    {
        //Spawning the current enemies wave
        if (wave < battleWaves.Length)
        {
            for (int i = 0; i < battleWaves[wave].waveEnemies.Length; i++)
            {
                Debug.Log("Enemy Spawned!!!");
                Instantiate(battleWaves[wave].waveEnemies[i]);
            }
        }
    }


    void FindPlayerUnits()
    {
        //Adding players to the array
        _Players = FindObjectsOfType<Player>();
        for (int i = 0; i < _Players.Length; i++)
        {
            _Players[i].ID = i; //Defining the character ID, which is going to be used to create the UIs;
            _Players[i].fillHUD(); //Changing the HUD based on the player character
            _Players[i].DisablePlayerHUD();
        }
    }

    void SettingPlayerUnitsPosition()
    {
        //Setting the players position
        for (int i = 0; i < PlayersPosition.Length; i++)
        {
            _Players[i].transform.position = PlayersPosition[i].position;
        }

    }

    void FindEnemyUnits()
    {
        //Adding enemies to the array

        _Enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < _Enemies.Length; i++)
        {
            _Enemies[i].ID = i; //Defining the enemy ID
        }
    }

    void SettingEnemyUnitsPosition()
    {
        //Setting the enemies position
        for (int i = 0; i < EnemiesPosition.Length; i++)
        {
            if (_Enemies[i] != null) //checking if the enemy exists
                _Enemies[i].transform.position = EnemiesPosition[i].position;
        }
    }

    #endregion
}