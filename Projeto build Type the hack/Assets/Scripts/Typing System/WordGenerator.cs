using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Word generator
*/
public class WordGenerator : MonoBehaviour
{
    public static bool isCustom = true;
    public static string[] wordList = { "malware", "bug", "virus", "error", "invasor", "corruption", "ataque", "infiltrado", "crash", "perigo", "alerta", "agressivo", "processador", "hack", "hacking", "ransomware", "keylogger", "spyware", "adware", "backdoor", "autorun", "kilim", "majava", "worms", "antivirus", "console", "log", "password", "senha", "script", "boot", "firewall", "navegador", "download", "exe", "arquivo", "clear", "cleaning", "tecnologia", "login", "processamento", "frame", "frames", "cpu", "pendrive", "driver", "monitor", "nuvem", "versionamento", "dados", "banco", "armazenamento", "armazenar", "processar", "hackear", "bloqueio", "aplicativo", "app", "programa", "comando", "prompt", "velocidade", "desenvolvimento", "algoritmo", "bigdata", "digitar", "cyber", "artificial", "ia", "iot", "backup", "bluetooth", "cookies", "homepage", "hashtag", "link", "inbox", "software", "hardware", "spam", "email", "redes", "rede", "sistema", "flame", "emotet", "info", "chave", "game", "video", "videogame", "controle", "algorithm", "application", "backend", "blockchain", "browser", "cache", "cloud", "code", "compiler", "cybersecurity", "database", "debugging", "deployment", "desktop", "encryption", "firewall", "framework", "frontend", "function", "gui", "html", "http", "ide", "interface", "javascript", "json", "kernel", "library", "linux", "logic", "machine", "learning", "metadata", "network", "poo", "operating", "system", "password", "protocol", "python", "query", "ram", "regression", "repository", "router", "saas", "scripting", "server", "software", "sql", "ssl", "stack", "syntax", "testing", "url", "ux", "ui", "version", "control", "virtualization", "vpn", "xml", "agile", "api", "bandwidth", "fix", "caching", "concurrency", "container", "data", "analysis", "mining", "science", "structure", "debug", "management", "dependency", "distributed", "computing", "handle", "handling", "full", "user", "high", "availability", "information", "security", "load", "balancing", "memory", "multithreading", "source", "performance", "optimization", "plugin", "responsive", "design", "scalability", "architecture", "test", "application", "hosting", "xss", "zero", "review", "point", "pointer", "reference", "binary", "one", "led", "keyboard", "cd" };

    public static List<string> wordsCustom = new List<string>();
    //public static string[] wordsCustom;

    char[] delimiterChars = { ',' };

    

    public static string GetRandomWord()
    {
        // if (isCustom)
        //{

            //int randomIndex = Random.Range(0, wordsCustom.Count);
            //string randomWord = wordsCustom[randomIndex];
            //return randomWord;
        //}
        //else
        //{
            int randomIndex = Random.Range(0, wordList.Length);
            string randomWord = wordList[randomIndex];
            return randomWord;

        //}
    }
}

