using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Word generator
*/
public class WordGenerator : MonoBehaviour
{
    public static bool isCustom = true;
    public static string[] wordList = {"malware", "bug", "virus", "error", "invasor", "corruption", "ataque", "infiltrado", "crash", "perigo", "alerta", "agressivo", "processador", "hack", "hacking", "ransomware", "keylogger", "spyware", "adware", "backdoor", "autorun", "kilim", "majava", "worms", "antivirus", "console", "log", "password", "senha", "script", "boot", "firewall", "navegador", "download", "exe", "arquivo", "clear", "cleaning", "tecnologia", "login", "processamento", "frame", "frames", "arquivos", "cpu", "pendrive", "driver", "monitor", "nuvem", "versionamento", "dados", "banco", "armazenamento", "armazenar", "processar", "hackear", "hackeando", "bloqueio", "aplicativo", "app", "programa", "comando", "prompt", "velocidade", "desenvolvimento", "algoritmo", "bigdata", "digitar", "cyber", "artificial", "ia", "iot", "backup", "bluetooth", "cookies", "homepage", "hashtag", "link", "inbox", "software", "hardware", "spam", "email", "redes", "rede", "sistema", "flame", "emotet", "info", "chave", "game", "video", "videogame", "controle" };

    public static List<string> wordsCustom = new List<string>();
    //public static string[] wordsCustom;

    char[] delimiterChars = { ',' };

    

    public static string GetRandomWord()
    {
        // if (isCustom)
        //{

            int randomIndex = Random.Range(0, wordsCustom.Count);
            string randomWord = wordsCustom[randomIndex];
            return randomWord;
        //}
        //else
        //{
            //int randomIndex = Random.Range(0, wordList.Length);
            //string randomWord = wordList[randomIndex];
            //return randomWord;

        //}
    }
}

