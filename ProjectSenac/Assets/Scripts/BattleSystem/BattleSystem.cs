using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BattleState {BATTLE_START, PLAYER_TURN, PLAYER_TARGET_SELECT, PLAYER_ACT, ENEMY_TURN, ENEMY_ACT, BATTLE_END}

public class BattleSystem : MonoBehaviour
{
    public BattleState state; //Defining the battle state
    public int turn; //Player and enemy characters turn

    [Header ("Players")]

    public PlayerUnit[] Players;
    public Transform[] PlayersPosition;
    public GameObject PlayersActUI;

    [Header("PlayerActions")]

    public int actOrbs; //number of orbs
    private PlayerActPoints playerActPoints;
    public List<Skills> playersActions; //List containing all the player's characters actions from this turn
    public List<Unit> playerSkillsTargets; //List containing all the player's skills targets


    [Header ("Enemies")]
    public EnemyUnit[] Enemies;
    public Transform[] EnemiesPosition;

    [Header("EnemiesActions")]
    public List<Skills> enemiesActions;
    public List <Unit> enemiesSkillsTargets;

    [Header("Enemy Wave System")]

    public int wave; //Battle number
    public int maxWaves; //Max number of battles
    public BattleWave[] battleWaves; //List of waves of enemies

    [Header("TargetSelect")]

    public MouseClick mouseClick; //get activated during the PLAYER_ACTION_SELECT state
    public SkillButton selectedButton; //the selected button with the selected skill

    void Start()
    {
        //Defining the number of waves in the battle
        maxWaves = battleWaves.Length;

        //Initializing battle
        state = BattleState.BATTLE_START;

        //Getting the PlayerActPoints component
        playerActPoints = GetComponent<PlayerActPoints>();

        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle() {

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
        state = BattleState.PLAYER_TURN;
        Debug.Log("Battle started, now it's the player's turn!");

    }

    #region Spawning Units

    void SpawnPlayers() { 
        //Method under develpment
    }
    void SpawnEnemies() {
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

        Players = FindObjectsOfType<PlayerUnit>();
        for (int i = 0; i < Players.Length; i++) {
            Players[i].ID = i; //Defining the character ID, which is going to be used to create the UIs;
            Players[i].fillHUD(); //Changing the HUD based on the player character
            Players[i].DisablePlayerHUD();
        }
    }

    void FindEnemyUnits()
    {
        //Adding enemies to the array

        Enemies = FindObjectsOfType<EnemyUnit>();
        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].ID = i; //Defining the enemy ID
        }
    }

    void SettingPlayerUnitsPosition()
    {
        //Setting the players position
        for (int i = 0; i < PlayersPosition.Length; i++) {
            Players[i].transform.position = PlayersPosition[i].position;
        }
        
    }

    void SettingEnemyUnitsPosition()
    {
        //Setting the enemies position
        for (int i = 0; i < EnemiesPosition.Length; i++)
        {
            if (Enemies[i] != null) //checking if the enemy exists
                Enemies[i].transform.position = EnemiesPosition[i].position;
        }
    }

    void CleaningDefeatedEnemies() {
        for (int i = 0; i < Enemies.Length; i++) {
            if(Enemies[i] != null)
                Destroy(Enemies[i].gameObject);
        }
        
    }
    #endregion

    void Update()
    {
        //Checking the current battle state
        switch (state) {
            case (BattleState.PLAYER_TURN):
                onPlayerTurn();
                break;
            case (BattleState.PLAYER_TARGET_SELECT):
                onTargetSelect();
                break;
            //Enemy battle state
            case (BattleState.ENEMY_TURN):
                onEnemyTurn();
                break;
            case (BattleState.ENEMY_ACT):
                //If all enemies "HasActed" boolean is true, then end act state
                bool HasAllActed = Enemies.All(Enemies => Enemies.HasActed == true);
                if (HasAllActed == true)
                {
                    Debug.Log("Enemy act finished");
                    //Cleaning enemy's lists
                    StartCoroutine(CleanEnemyLists());
                    //Ending the battle looping
                    StartCoroutine(OnBattleEnd());
                }
                break;
        }
    }



    #region Player_Turn
    void onPlayerTurn() {
        //Enabling the player's UI
        PlayersActUI.SetActive(true);

        Players[turn].EnablePlayerHUD();
    }

    void onTargetSelect() {
        mouseClick.gameObject.SetActive(true);
    }

    public void addSkill(Skills sk, int TargetType, Unit unit) {
        //adding the selected skill to the list
        if (playersActions.Count >= Players.Length)
        {
            playersActions[turn] = sk;
        }
        else
        {
            playersActions.Add(sk);
        }

        playerSkillsTargets.Add(unit);
    }

    public void onPlayerSelectAction(){
        Debug.Log("Action selected!");
        //Disabling the selected character's UI
        Players[turn].DisablePlayerHUD();
        //Getting the next player character
        turn++;
        //if the value is greater than the number of players, then the acting turn occurs
        if (turn < Players.Length)
        {
            //check if the unit is dead
            if (Players[turn].isDead == false)
                onPlayerTurn();
            else {
                Debug.Log(Players[turn].unitStat.UnitName + " is dead and can't act!");
                onPlayerSelectAction();
            }
        }
        else
        {
            //Starting the player acting turn
            turn = 0;
            state = BattleState.PLAYER_ACT;
            StartCoroutine(onPlayerAct());
        }
    }
    #endregion

    #region Player_Act
    IEnumerator onPlayerAct() {
        for (int i = 0; i < Players.Length; i++){
            //Animating the player character
            ////Players[i].GetComponent<Animator>().SetBool("Acting", true);

            //Calling the skill
            Debug.Log("Calling the skills!");
            if (playersActions[i].skillTargetType == 3)
            {
                //Targeting all the player characters
                for (int j = 0; j < Players.Length; j++)
                {
                    onCallPlayerSkill(playersActions[i], Players[i], Players[j]);
                }
            }
            else if (playersActions[i].skillTargetType == 4)
            {
                //Targeting all the enemies
                for (int j = 0; j < Enemies.Length; j++)
                {
                    onCallPlayerSkill(playersActions[i], Players[i],    Enemies[j]);
                }
            }
            else
            {
                //Targeting only one unit
                onCallPlayerSkill(playersActions[i], Players[i], playerSkillsTargets[i]);
            }

            yield return new WaitForSeconds(1f);
        }
        //Reseting the Action Points
        playerActPoints.ResetActOrbs();

        //Reseting the player's characters targets
        playerSkillsTargets.Clear();

        //Starting the enemy turn
        state = BattleState.ENEMY_TURN;
    }

    void onCallPlayerSkill(Skills sk, PlayerUnit pl, Unit target)
    {
        //defining if the player will hit the target or not
        float hit = Random.Range(0f, 100f);

        //Call the skill if the skill hit the target
        if (hit <= sk.accuracyRate)
        {
            //defining if the target will be hitted by a critical hit
            float crit = Random.Range(0f, 100f);
            float critBonus = 0;

            if (crit <= sk.criticalRate)
            {
                critBonus = sk.damage / 2; //damage bonus when get a critical hit
                Debug.Log("Wow! It's a critical hit!");
            }

            //Using the skill
            switch (sk.Name)
            {
                case "ATTACK":
                    Debug.Log(pl.unitStat.UnitName + " used " + sk.Name);
                    target.TakeDamage(sk.damage + critBonus);
                    target.anim.SetTrigger("DAMAGE"); //general animation
                    Debug.Log(target.unitStat.UnitName + " took damage!");
                    break;
                case "HEAL":
                    Debug.Log(pl.unitStat.UnitName + " used " + sk.Name);
                    target.HP += sk.damage;
                    target.anim.SetTrigger("HEAL");
                    Debug.Log(target.unitStat.UnitName + " was healed!");
                    break;
                case "AUTOHEALING":
                    Debug.Log(pl.unitStat.UnitName + " used " + sk.Name);
                    target.Healing(sk.damage);
                    target.anim.SetTrigger("HEAL");
                    Debug.Log(target.unitStat.UnitName + " has autohealed!");
                    break;
                case "BUFF":
                    Debug.Log(pl.unitStat.UnitName + " used " + sk.Name);
                    target.stef = statusEffect.REGEN; //Adding the buff's effect to the target
                    target.statEffectDuration = sk.effectDuration; //Adding the buff's duration
                    target.anim.SetTrigger("BUFF");
                    Debug.Log(target.unitStat.UnitName + " received a buff!");
                    break;
                case "DEBUFF":
                    Debug.Log(pl.unitStat.UnitName + " used " + sk.Name);
                    target.stef = statusEffect.POISON; //Adding the debuff's effect to the target
                    target.statEffectDuration = sk.effectDuration; //Adding the debuff's duration
                    target.anim.SetTrigger("DEBUFF");
                    Debug.Log(target.unitStat.UnitName + " received a debuff!");
                    break;
                case "HEALALL":
                    target.Healing(sk.damage);
                    target.anim.SetTrigger("HEAL");
                    Debug.Log(target.unitStat.UnitName + " was healed!");
                    break;
                case "SKIPTURN":
                    Debug.Log(pl.unitStat.UnitName + " is waiting...");
                    break;
            }
        }
        else {
            target.anim.SetTrigger("EVADE"); //general animation
            Debug.Log(pl.unitStat.UnitName + " missed the attack!");
        }
    }

    #endregion

    #region Enemy_Turn
    //1. Check how many enemies are in the list
    //2. Save their actions
    //3. In the last one, finish the turn
    void onEnemyTurn()
    {
        //Check if all enemies are dead
        bool allEnemiesDead = Enemies.All(Enemies => Enemies.isDead == true);
        if(allEnemiesDead == true)
        {
            //The battle is won!
            StartCoroutine(OnBattleEnd());
        }   
        

        //Check what enemies are dead and play their functions
        if (turn < Enemies.Length)
        {
            foreach (EnemyUnit enemyUnit in Enemies)
            {
                    if (enemyUnit.isDead == true)
                    {
                        enemyUnit.HasActed = true;
                        //Enemies[enemyUnit.ID] = null;
                        turn++;
                    }
                    else if (enemyUnit.isDead == false)
                    {
                        enemyUnit.HasActed = false;
                        if (enemyUnit.HasActed == false)
                        {
                            enemyUnit.IA_Logic();
                        }
                        turn++;
                    }
            }
        }
        else 
        {
            Debug.Log("Enemy turn finished. Start enemyAct");
            turn = 0;
            StartCoroutine(onEnemyAct());
            state = BattleState.ENEMY_ACT;
        }

    }

    //Clean the enemies's lists after the enemy act
    IEnumerator CleanEnemyLists()
    {
        enemiesActions.Clear();
        enemiesSkillsTargets.Clear();
        yield return new WaitForSeconds(1);
    }

    //Add skills for the enemies
    public void addSkillEnemy(Skills sk, Unit unit)
    {
        //adding the selected skill to the list
        enemiesActions.Add(sk);

        enemiesSkillsTargets.Add(unit);
    }
    //Skill with multiple targets
    public void addSkillEnemy(Skills sk)
    {
        //adding the selected skill to the list
        enemiesActions.Add(sk);
    }


    #endregion

    #region Enemy_Act
    //1. Play each action consecutively
    //2. If all the enemies did their actions, change to player's turn

    IEnumerator onEnemyAct()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            //Animating the enemy character
            ////Enemies[i].GetComponent<Animator>().SetBool("Acting", true);

            //Calling the skill
            Debug.Log("Calling the enemy skills!");
            if (enemiesActions[i].skillTargetType == 3)
            {
                //Targeting all the enemies 
                for (int j = 0; j < Enemies.Length; j++)
                {
                    onCallEnemySkill(enemiesActions[i], Enemies[i], Enemies[j]);
                }
            }
            else if (enemiesActions[i].skillTargetType == 4)
            {
                //Targeting all the players characters
                for (int j = 0; j < Players.Length; j++)
                {
                    onCallEnemySkill(enemiesActions[i], Enemies[i], Players[j]);
                }
            }
            else
            {
                //Targeting only one unit
                onCallEnemySkill(enemiesActions[i], Enemies[i], enemiesSkillsTargets[i]);
            }

            yield return new WaitForSeconds(1f);
        }


        if (Enemies.Length == 1)
        {
            if (Enemies[0].HasActed == false && Enemies[0].isDead == false)
            {
                Debug.Log("Calling the enemy skills!");
                onCallEnemySkill(enemiesActions[0], Enemies[0], enemiesSkillsTargets[0]);
            }
            yield return new WaitForSeconds(1f);

        }
        else
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                //Calling the skill
                if (Enemies[i].HasActed == false && Enemies[i].isDead == false) 
                {
                    Debug.Log("Calling the enemy skills!");
                    onCallEnemySkill(enemiesActions[i], Enemies[i], enemiesSkillsTargets[i]);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void onCallEnemySkill(Skills sk, EnemyUnit en, Unit target)
    {
        en.HasActed = true;
        switch (sk.Name)
        {
            case "ATTACK":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name + " in " + target.name);
                target.TakeDamage(sk.damage);
                target.anim.SetTrigger("DAMAGE"); //general animation
                break;
            case "STEALLIFEATTACK":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name);
                target.TakeDamage(sk.damage);
                en.Healing(sk.damage - 1);
                Debug.Log(target.unitStat.UnitName + " took damage! And " + en.unitStat.UnitName + " gain life");
                target.anim.SetTrigger("DAMAGE");
                break;
            case "HEAL":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name + " in " + target.name);
                target.Healing(sk.damage);
                target.anim.SetTrigger("HEAL");
                break;
            case "HEALALL":
                target.Healing(sk.damage);
                Debug.Log(target.unitStat.UnitName + " was healed!");
                target.anim.SetTrigger("HEAL");
                break;
            case "AUTOHEALING":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name + " in " + target.name);
                target.Healing(sk.damage);
                target.anim.SetTrigger("HEAL");
                break;
            case "BUFF":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name + " in " + target.name);
                target.stef = statusEffect.REGEN; //Adding the buff's effect to the target
                target.statEffectDuration = sk.effectDuration; //Adding the buff's duration
                target.anim.SetTrigger("BUFF");
                Debug.Log(target.unitStat.UnitName + " received a buff!");
                break;
            case "DEBUFF":
                Debug.Log(en.unitStat.UnitName + " used " + sk.Name + " in " + target.name);
                target.stef = statusEffect.POISON; //Adding the debuff's effect to the target
                target.statEffectDuration = sk.effectDuration; //Adding the debuff's duration
                target.anim.SetTrigger("DEBUFF");
                Debug.Log(target.unitStat.UnitName + " received a debuff!");
                break;
            case "SKIPTURN":
                Debug.Log(en.unitStat.UnitName + " is waiting...");
                target.anim.SetTrigger("EVADE");
                break;

        }
    }

    #endregion

    #region Battle_End

    IEnumerator OnBattleEnd() {

        Debug.Log("Ending Looping");
        state = BattleState.BATTLE_END;

        yield return new WaitForSeconds(1f);

        //Checking if the player lost the battle

        int nDefeatedPlayers = 0;

        for (int i = 0; i < Players.Length; i++)
        {
            //Checking player units status effects
            Players[i].CheckStatusEffect();

            //Checking if all the players are defeated
            if (Players[i].isDead)
            {
                nDefeatedPlayers++;
            }
        }

        if (nDefeatedPlayers == Players.Length)
        {
            //GAME OVER!
            Debug.Log("GAME OVER!");
            StartCoroutine(OnGameOver());
        }
        else
        {
            //Checking if the player won the battle

            int nDefeatedEnemies = 0; //number of dead enemies

            for (int i = 0; i < Enemies.Length; i++)
            {
                //Checking enemy units status effects
                Enemies[i].CheckStatusEffect();

                //Checking if all the enemies are defeated
                if (Enemies[i].isDead)
                {
                    nDefeatedEnemies++;
                }
            }

            if (nDefeatedEnemies == Enemies.Length)
            {
                wave++;
                if (wave >= maxWaves)
                {
                    //End Battle -> Player Won!
                    CleaningDefeatedEnemies();
                    Debug.Log("You won the battle!!! Battle ended");
                }
                else
                {
                    //Spawn the next wave of enemies
                    Debug.Log("Starting the wave " + (wave + 1) + "!");
                    CleaningDefeatedEnemies();
                    SpawnEnemies();
                    FindEnemyUnits();
                    SettingEnemyUnitsPosition();

                    //Restarting the battle looping
                    state = BattleState.PLAYER_TURN;
                }
            }
            else
            {
                //Restarting the battle looping
                Debug.Log("Restarting the battle looping");
                state = BattleState.PLAYER_TURN;
            }
        }

    }

    IEnumerator OnGameOver() {
        //End the battle and open the game over screen
        yield return new WaitForSeconds(1f);

        //Open the game over screen
    }

    #endregion
}
