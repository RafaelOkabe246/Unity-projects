
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public WordTimer wordTimer;
    public float delayWave;
    public int waveNumber;
    public ColorRandomizer colorRandomizer;
    public bool hasWon;

    [Header("Progressive increase")]
    public int progressiveIncrease;
    public float specialEnemyChance;

    public int countToIncrease;

    public int bossWave;

    public Animator visualEffectAnim;
    public TextMeshProUGUI visualEffectText;


    private void Start()
    {
        hasWon = false;
        waveNumber = 0;
        countToIncrease = 0;
    }

    public IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(delayWave);
        PlayNewWave();
    }


    void PlayNewWave()
    {
        waveNumber += 1;

        colorRandomizer.RandomizeColor();

        StartCoroutine(VictoryEffects());
        if (hasWon)
            return;

        wordTimer.enemiesSpawned = 0;

        countToIncrease++;
        if (countToIncrease == 2)
        {
            wordTimer.enemiesCountInWave += progressiveIncrease;
            if (wordTimer.enemiesCountInWave > 15)
                wordTimer.enemiesCountInWave = 15;
            countToIncrease = 0;
        }

        if (wordTimer.wordManager.wordSpawner.chanceSpecialEnemy + specialEnemyChance <= 35)
            wordTimer.wordManager.wordSpawner.chanceSpecialEnemy += specialEnemyChance;

        if (waveNumber == bossWave)
        {
            //Activate boss fight
            wordTimer.lastEnemiesCount = wordTimer.enemiesCountInWave;
            wordTimer.wordManager.wordSpawner.isBossFight = true;
            wordTimer.enemiesCountInWave = 1;
            bossWave += 5;
            return;
        }
        
        //After boss wave
        else if (wordTimer.wordManager.wordSpawner.isBossFight == true) 
        {
            wordTimer.wordManager.wordSpawner.isBossFight = false;
            wordTimer.enemiesCountInWave = wordTimer.lastEnemiesCount - 1;
        }

        Debug.Log("Enemies in wave: " + wordTimer.enemiesCountInWave);


    }

    private IEnumerator VictoryEffects() {

        if (hasWon)
        {
            //Won the game, load the victory screen
            visualEffectAnim.SetTrigger("On");
            visualEffectText.text = "ULTIMO INVASOR DESTRUIDO";
            SoundSystem.instance.SearchSound(2, "Audio_waves");
            yield return new WaitForSeconds(1f);

            string _message = "Carregando recompensa";
            ConsoleManager.consoleManager.CallConsole(_message, Color.yellow, true);

            yield return new WaitForSeconds(2f);

            StartCoroutine(LevelLoader.LoadLevel(6));
        }
        else
        {
            visualEffectAnim.SetTrigger("On");
            visualEffectText.text = "ONDA " + waveNumber + " COMPLETA";
            SoundSystem.instance.SearchSound(2, "Audio_waves");
            yield return new WaitForSeconds(1f);

            string message = "Detectando uma nova onda de virus. Iniciando onda " + (waveNumber + 1);
            ConsoleManager.consoleManager.CallConsole(message, Color.yellow, true);
        }
    }
}
