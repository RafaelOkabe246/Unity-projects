using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripulantesStatusController : MonoBehaviour
{
    // Control the status of each member of the crew
    public MainGameController _MainGameController;

    public Tripulante[] Tripulantes;
    public bool[] IsAlive;
    public bool EveryoneDied;
 
    public int HungryDecreaseValue;
    public int ThirstDecreaseValue;
    public int SanityDecreaseValue;

    private void Start()
    {
        HungryDecreaseValue *= -1;
        ThirstDecreaseValue *= -1;
        SanityDecreaseValue *= -1;
    }

    private void Update()
    {
        CheckCondition();
    }

    public void DecreaseAllTripulanteStatus()
    {
        foreach(Tripulante tripulante in Tripulantes)
        {
            if (tripulante.Hungry < 3 || tripulante.Thirst < 3)
            {
                tripulante.Sanity = tripulante.AffectStatus(SanityDecreaseValue, tripulante.Sanity);
            } 
            else if (tripulante.Hungry > 3 || tripulante.Thirst > 3)
            {
                if (tripulante.Sanity < 5)
                {
                    tripulante.Sanity = tripulante.AffectStatus(SanityDecreaseValue * -1, tripulante.Sanity);
                }
            }
            tripulante.Hungry = tripulante.AffectStatus(HungryDecreaseValue, tripulante.Hungry);
            tripulante.Thirst = tripulante.AffectStatus(ThirstDecreaseValue, tripulante.Thirst);

        }
    }

    public void CheckCondition()
    {
        EveryoneDied = AreAllDead();
        //All alive conditions are equal to the crew condition
        for (int i = 0; i < Tripulantes.Length; i++)
        {
            IsAlive[i] = Tripulantes[i].IsTripulanteAlive; 
        }
       
    }
    
    public void UpdateCondition()
    {
        //Set the bool to false
        for (int i = 0; i < Tripulantes.Length; i++)
        {
            if (Tripulantes[i].IsTripulanteAlive == false)
            {
                IsAlive[i] = false;
            }
        }

    }
    
    private bool AreAllDead()
    {
        for (int i = 0; i < IsAlive.Length; ++i)
        {
            if (IsAlive[i] == true)
            {
                return false;
            }
        }
        return true;
    }

    public void EndTurnEffects()
    {
        DecreaseAllTripulanteStatus();
        UpdateCondition();
    }
}
