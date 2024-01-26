using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Animator sceneTransition;
   
    private void Awake()
    {
        instance = this;
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

    private IEnumerator LoadLevelCoroutine(int level)
    {
        yield return new WaitForSeconds(0.25f);

        //Start scene transtion animations
        if (!sceneTransition)
            sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
        sceneTransition.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
    }

}
