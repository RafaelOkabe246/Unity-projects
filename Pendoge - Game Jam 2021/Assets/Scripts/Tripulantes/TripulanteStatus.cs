using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripulanteStatus : MonoBehaviour
{
    [SerializeField]
    private Tripulante _tripulante;

    public int HungryDecreaseValue;
    public int ThirstDecreaseValue;
    public int SanityDecreaseValue;

    private void Start()
    {
        _tripulante = this.GetComponent<Tripulante>();
    }

    public void DecreaseStatus()
    {
        _tripulante.Hungry = _tripulante.AffectStatus(HungryDecreaseValue, _tripulante.Hungry);
        _tripulante.Thirst = _tripulante.AffectStatus(ThirstDecreaseValue, _tripulante.Thirst);
    }


}
