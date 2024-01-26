using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
* The word displayed on scene, using TextMeshPro
*/
public class WordDisplay : MonoBehaviour
{
    public TextMeshPro text;
    //public float fallSpeed = 1f;
    public Enemy parentEnemy;
    public Word enemyWord;
    public TextMeshPro shadowText;

    public virtual void SetWord(string word)
    {
        text.text = word;
        shadowText.text = text.text;
    }

    public virtual void RemoveLetter()
    {
        if (text.text.Length > 0)
        {
            text.text = text.text.Remove(0, 1);
            shadowText.text = text.text;
            if (parentEnemy != null)
            {
                parentEnemy.CallParticles();
                parentEnemy.CallLightAnimation();
            }
            ChangeColor(Color.red);
        }
    }

    public void ChangeColor(Color color) {
        text.color = color;
    }

    public virtual void RemoveWord()
    {
        parentEnemy.DescreaseLife();
        if (parentEnemy.enemyLifes <= 0)
        {
            SoundSystem.instance.SearchSound(0, "Audio_bomb");
            parentEnemy.DestroyEnemy();
            Destroy(gameObject);
        }
        else {
            WordManager wordManager = FindObjectOfType<WordManager>();
            SoundSystem.instance.SearchSound(0,"Audio_damage_enemy");
            wordManager.ReAddWord();
        }

    }

}
