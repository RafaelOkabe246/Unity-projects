using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleLog : MonoBehaviour
{
    public TextMeshPro consoleText;
    private string currentText;
    private string fullText;
    public float textDelay;

    public TextMeshPro logText;
    public SpriteRenderer logSprite;

    public void SetConsoleText(string text, Color color, int layerBonus) {
        consoleText.sortingOrder += layerBonus;
        logText.sortingOrder += layerBonus;
        logSprite.sortingOrder += layerBonus;

        consoleText.color = color;
        fullText = text;
        StartCoroutine(WriteText());
    }

    private IEnumerator WriteText() {
        for (int i = 0; i < fullText.Length; i++) {
            currentText = currentText + fullText[i];
            consoleText.text = currentText;
            yield return new WaitForSeconds(textDelay);
        }
    }
}
