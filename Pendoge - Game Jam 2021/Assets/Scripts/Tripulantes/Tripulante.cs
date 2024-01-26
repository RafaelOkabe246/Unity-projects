using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tripulante : MonoBehaviour 
{
    [Header("Expressions")]
    public Sprite Tripulante_dead;
    public Sprite Tripulante_idle;
    public Sprite Tripulante_crazy;
    public Sprite Tripulante_happy;
    public Sprite Tripulante_angry;
    public Sprite Tripulante_sad;


    [Header("Status value")]
    public float Sanity;
    public float Hungry;
    public float Thirst;

    public TextMeshProUGUI HungryText;
    public TextMeshProUGUI ThirstText;
    public TextMeshProUGUI SanityText;

    public int Index;

    public bool IsTripulanteAlive;

    [SerializeField] private Slider sanitySlider;
    [SerializeField] private Slider hungrySlider;
    [SerializeField] private Slider thirstSlider;

 

    private void Update()
    {
        sanitySlider.value = Sanity/10;
        hungrySlider.value = Hungry/10;
        thirstSlider.value = Thirst/10;

        ExpressionManager(Sanity, Hungry, Thirst);

        if (Sanity < 0 || Hungry < 0 || Thirst < 0)
        {
            IsTripulanteAlive = false;
            
        }

        HungryText.text = "Fome: " + Hungry;
        ThirstText.text = "Sede: " + Thirst;
        SanityText.text = "Sanidade: " + Sanity;
    }

    /*
    public void UsingItem(Item item)
    {
        switch (item._Tipo_De_Status)
        {
            case (Tipo_de_status.Hungry):
                Hungry = AffectStatus(item.StatusAffectValue, Hungry);
                break;1

            case (Tipo_de_status.Thirst):
                Thirst = AffectStatus(item.StatusAffectValue, Thirst);
                break;

            case (Tipo_de_status.Sanity):
                Sanity = AffectStatus(item.StatusAffectValue, Sanity);
                break;
        }

    }
    */

    public float AffectStatus(int AffectPoints, float StatusType)
    {
        float result = StatusType += AffectPoints;
        return result;
    }
    

    public void EventAffectStatus(Event _event)
    {
        switch (_event._StatusType)
        {
            case (StatusType.Hungry):
                Hungry = AffectStatus(_event.StatusAffectValue, Hungry);
                break;

            case (StatusType.Thirst):
                Thirst = AffectStatus(_event.StatusAffectValue, Thirst);
                break;

            case (StatusType.Sanity):
                Sanity = AffectStatus(_event.StatusAffectValue, Sanity);
                break;
        }
    }

    void ExpressionManager(float sanity, float hungry, float thirst)
    {
        if (hungry > 3 && sanity > 2 || thirst > 3 && sanity > 2)
        {
            this.gameObject.GetComponent<Image>().sprite = Tripulante_idle;
        }
        else if (hungry < 3 || thirst < 3)
        {
            this.gameObject.GetComponent<Image>().sprite = Tripulante_sad;
        }
        if (IsTripulanteAlive == false)
        {
            //this.gameObject.GetComponent<Image>().sprite = Tripulante_dead;
            gameObject.SetActive(false);
        }

    }

}
