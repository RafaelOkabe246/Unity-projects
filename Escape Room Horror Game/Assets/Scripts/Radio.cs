using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Radio : MonoBehaviour
{
    public Player player;
    public Animator doorAnim;

    [Header("Radio frequency")]
    public TextMesh numberFrequecyNumber;
    

    [Header("Passoword")]
    [SerializeField] int passwordNumberIndex;
    public int[] senhaDigitada;
    public int[] senha;
    public bool[] hasTyppedNumber;
    public bool isCorrect;

    public AudioClip[] typpingClips;
    public AudioClip wrongPasswordClip;
    public AudioClip correctPasswordClip;
    public AudioClip newNumberClip;



    private void Start()
    {
        StartCoroutine(UpdateFrequencyValue());

        for (int i = 0; i < senha.Length; i++)
        {
            senha[i] = Random.Range(0,9);
            hasTyppedNumber[i] = false;
            UiManager.instance.passwordShowText.text += senha[i];

        }
    }


    public void TyppingNumber(int number)
    {
        SoundManager.instance.PlaySFX(typpingClips[Random.Range(0, typpingClips.Length - 1)]);
        if (hasTyppedNumber[passwordNumberIndex] == false)
        {
            hasTyppedNumber[passwordNumberIndex] = true;
            senhaDigitada[passwordNumberIndex] = number - 1;

            if (passwordNumberIndex < hasTyppedNumber.Length - 1)
                passwordNumberIndex += 1;
            else if (passwordNumberIndex == hasTyppedNumber.Length - 1)
                CheckPassword();
        }
    }
    void CheckPassword()
    {
        for (int i = 0; i < senhaDigitada.Length; i++)
        {
            if (senha[i] != senhaDigitada[i])
            {
                //Senha errada, resete os números
                ResetPassword();
                return;
            }
        }

        //The password is correct, you won the game
        SoundManager.instance.PlaySFX(correctPasswordClip);
        doorAnim.SetBool("isDisable", true);
        isCorrect = true;
        Debug.Log("You win");
    }
    public void ResetPassword()
    {
        
        SoundManager.instance.PlaySFX(wrongPasswordClip);
        for (int x = 0; x < hasTyppedNumber.Length; x++)
        {
            hasTyppedNumber[x] = false;
        }

        for (int i = 0; i < senhaDigitada.Length; i++)
        {
            senhaDigitada[i] = 0;
        }
        passwordNumberIndex = 0;

        //Emite um som que atraí os monstros
        //MonsterSpawn.soundDanger += 1;
    }


    int currentNumberIndex = 0;
    int newmNumberIndex;
    public IEnumerator UpdateFrequencyValue()
    {
        float waiting = Random.Range(3, 5);
        yield return new WaitForSeconds(waiting);

        //Update number
        if (currentNumberIndex == senha.Length - 1)
        {
            currentNumberIndex = 0;
        }
        else if (currentNumberIndex < senha.Length - 1)
        {
            currentNumberIndex += 1;
        }

        newmNumberIndex = currentNumberIndex;
        int newNumber = senha[newmNumberIndex];

        numberFrequecyNumber.text = "" + newNumber;

        StartCoroutine(TextEffect());

        StartCoroutine(UpdateFrequencyValue());
    }

    IEnumerator TextEffect()
    {
        if (player._GameMode == GameMode.Radio)
            SoundManager.instance.PlaySFX(newNumberClip);

        yield return new WaitForSeconds(0.1f);
        numberFrequecyNumber.color = new Color(1f,1f,1f,0.5f);
        yield return new WaitForSeconds(0.1f);
        numberFrequecyNumber.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        numberFrequecyNumber.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        numberFrequecyNumber.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        numberFrequecyNumber.color = new Color(1f, 1f, 1f, 1f);
    }

}
