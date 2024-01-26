using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject AttackButton;
    public GameObject HealButton;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit PlayerUnit;
    Unit EnemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;


    public string NarrativeScene;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        PlayerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        EnemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Você está enfrentando um(a)" + EnemyUnit.unitName;

        playerHUD.SetHUD(PlayerUnit);
        enemyHUD.SetHUD(EnemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    IEnumerator PlayerAttack()
    {

        //Damage the enemy
        bool isDead = EnemyUnit.Takedamage(PlayerUnit.damage);

        enemyHUD.SetHP(EnemyUnit.currentHP);
        dialogueText.text = "O ataque foi um sucesso";
        
        AttackButton.SetActive(false);
        HealButton.SetActive(false);
       
        yield return new WaitForSeconds(2f);


        //Check if the enemy is dead
        if(isDead == true)
        {
            //End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        //Change state base on what happens
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Você venceu";
            SceneManager.LoadScene(NarrativeScene);
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "Cada queda, é um degrau para a vitória";
            SceneManager.LoadScene(NarrativeScene);
        }
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = "É a vez de" + EnemyUnit.unitName;

        yield return new WaitForSeconds(1f);

        bool isDead = PlayerUnit.Takedamage(EnemyUnit.damage);

        playerHUD.SetHP(PlayerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        //Check if the player is dead
        if (isDead == true)
        {
            //End the battle
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    //Player functions
    void PlayerTurn()
    {
        AttackButton.SetActive(true);
        HealButton.SetActive(true);
        dialogueText.text = "Desce a porrada nele";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());

    }

    IEnumerator PlayerHeal()
    {

        PlayerUnit.Heal(5);

        playerHUD.SetHP(PlayerUnit.currentHP);
        dialogueText.text = "Você recuperou" + 5 + "de vida";

        AttackButton.SetActive(false);
        HealButton.SetActive(false);

        yield return new WaitForSeconds(1f);


        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
}
