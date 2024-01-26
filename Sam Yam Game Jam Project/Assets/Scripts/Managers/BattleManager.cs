using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Battle battlePrefab;
    public static BattleManager instance;

    public List<Battle> _battles;
    public Battle _battle;

    private void Awake()
    {
        instance = this;
    }

    public void StartBattle(Unit attacker, Unit defender, TerrainType battleTerrain)
    {
        Battle newBattle = Instantiate(battlePrefab, transform.position, Quaternion.identity);
        newBattle.Attacker = attacker;
        newBattle.Defender = defender;
        newBattle.battleTerrain = battleTerrain;
        //_battles.Add(newBattle);

        _battle = newBattle;

        Debug.Log("Nem battle added");

        //Play battle animation
        PlayBattle();
    }

    public void PlayBattle()
    {
        //Show animation and make the units clash

        //That void will be called during the animation
        _battle.BattleResult();

    }

    public void AddUnitToBattle(Unit reinforcementUnit)
    {

    }
}
