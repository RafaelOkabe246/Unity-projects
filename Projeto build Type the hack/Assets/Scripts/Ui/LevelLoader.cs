using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Transition", 0);
        
    }

    #region Methods

    public void OnLoadThisLevel()
    {
        StartCoroutine(LoadThisLevel());
    }

    public void OnLoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void OnLoadLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    public void OnLoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void OnExitGame()
    {
        StartCoroutine(ExitGame());
    }

    #endregion

    #region Coroutines

    public IEnumerator LoadThisLevel()
    {

        yield return new WaitForSeconds(0.8f);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public static IEnumerator ExitGame()
    {

        yield return new WaitForSeconds(0.8f);

        Application.Quit();
    }

    public static IEnumerator LoadLevel(int levelIndex)
    {
        //Load any level
        anim.SetInteger("Transition", 1);

        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene(levelIndex);
    }

    #endregion
}
