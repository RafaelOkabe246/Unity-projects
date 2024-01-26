using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    public static ConsoleManager consoleManager;

    public GameObject consoleLogPrefab;
    private int consoleCount = 0;
    public float timeToDestroyConsole;
    public static bool isConsoleOpen = false;

    void Start()
    {
        consoleManager = this;
    }

    public void CallConsole(string logText, Color textColor, bool hasPriority) {
        if (!isConsoleOpen || hasPriority)
        {
            consoleCount++;
            GameObject console = Instantiate(consoleLogPrefab);
            console.GetComponent<ConsoleLog>().SetConsoleText(logText, textColor, consoleCount);
            isConsoleOpen = true;
            StartCoroutine(OnCloseConsole());
            Destroy(console, timeToDestroyConsole);
        }
    }

    IEnumerator OnCloseConsole() {
        yield return new WaitForSeconds(timeToDestroyConsole - 2);
        isConsoleOpen = false;
    }
}
