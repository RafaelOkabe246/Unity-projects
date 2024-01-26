using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Unit Defender;

    public Unit Attacker; 

    public TerrainType battleTerrain;

    public void BattleResult()
    {

        int attackerValueDamage = rollDamage(Attacker._unitStats.Defense + Attacker._unitStats.Attack + Attacker._unitStats.Charge);
        int defenderValueDamage = rollDamage(Defender._unitStats.Defense + Defender._unitStats.Attack + battleTerrain.defenseBonus);

        Debug.Log(attackerValueDamage);
        Debug.Log(defenderValueDamage);

        int attackerHpResult = Attacker._HP -= defenderValueDamage;
        int defenderHpResult = Defender._HP -= attackerValueDamage;


        //Destroy the dead units
        if(attackerHpResult <= 0 && defenderHpResult <= 0)
        {
            //As duas tropas morreram   
            Debug.Log("As duas tropas morreram");
            Destroy(Defender.gameObject);
            Destroy(Attacker.gameObject);
        }
        else if (attackerHpResult <= 0)
        {
            //Uma das tropas morreu
            Debug.Log("O atacante morreu");
            Destroy(Attacker.gameObject);
        }
        else if (defenderHpResult <= 0)
        {
            //Uma das tropas morreu
            Debug.Log("O defensor morreu");
            Destroy(Defender.gameObject);
        }

    }

    int result;
    int rollDamage(int value)
    {

        for (int i = 0; i < value; i++)
        {
            int sucess = Random.Range(0, 2);

            if (sucess == 1)
            {
                result += 1;
            }
            else if(sucess == 0)
            {
                //Fail
            }

        }
        return result;
    }
    
}
