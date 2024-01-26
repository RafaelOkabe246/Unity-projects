using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public const int introductionSceneIndex = 0;
    public const int mainMenuSceneIndex = 1;
    public const int tabuleiroSceneIndex = 2;
    public const int endlessRunnerSceneIndex = 3;
    public const int parySceneIndex = 4;
    public const int combatSceneIndex = 5;
    public const int gameOverSceneIndex = 6;




    public static SceneManagement instance;
    public Animator sceneTransition;

    private void Awake()
    {
            instance = this;

        sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
        StartCoroutine(StartLevel());
    }


    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    public void LoadCurrenttLevel()
    {
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadLevel()
    {
        StartCoroutine(ReloadLevelCoroutine());
    }

    private IEnumerator LoadLevelCoroutine(int level)
    {
        //Start scene transtion animations
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
        yield return new WaitForSeconds(2f);

        sceneTransition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(level);
    }

    private IEnumerator ReloadLevelCoroutine()
    {
        //Start scene transtion animations
        //ScreenStack.instance.ClearScreenStack();
        sceneTransition.Play("ANIM_FadeIn");

        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(0.5f);
        sceneTransition.Play("ANIM_FadeOut");
    }
}
