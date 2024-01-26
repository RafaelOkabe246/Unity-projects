using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UiSystem uiSystem;
    public WordManager wordManager;
    Animator playerAnim;
    public GameObject playerHolder;
    public static Animator playerHolderAnim;

    private void Start()
    {
        PointSystem.ResetPoints();
        playerAnim = GetComponent<Animator>();
        playerHolderAnim = playerHolder.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Toma dano ou morre
            SoundSystem.instance.SearchSound(2, "Audio_damage_robot");
            StartCoroutine(CameraController.CameraShake(2f, 2f, 0.5f));
            StartCoroutine(GameOver());
            
        }
    }

    public static void CallPlayerTapAnimation() {
        playerHolderAnim.SetTrigger("Tap");
    }

    private IEnumerator GameOver() {
        playerAnim.SetTrigger("GameOver");
        WordManager wordManager = FindObjectOfType<WordManager>();
        wordManager.cantType = true;

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy en in enemies) {
            en.cantMove = true;
        }

        string message = "Uma ameaça foi detectada! Sistema invadido por malwares. Reinicializando";
        ConsoleManager.consoleManager.CallConsole(message, Color.red, true);

        yield return new WaitForSeconds(5f);
        StartCoroutine(LevelLoader.LoadLevel(2));
    }
}
