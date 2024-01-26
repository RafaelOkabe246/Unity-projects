using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
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
        ScreenStack.instance.ClearScreenStack();
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
        sceneTransition.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
    }

    private IEnumerator ReloadLevelCoroutine() 
    {
        //Start scene transtion animations
        ScreenStack.instance.ClearScreenStack();
        sceneTransition.Play("ANIM_FadeIn");

        yield return new WaitForSeconds(1f);

        PlayerSpawner.instance.SpawnPlayerAtPosition(StageBlocksHandler.savedCurrentBlock.startPoint.position);

        StageBlocksHandler.savedCurrentBlock.ReloadStageBlock();

        yield return new WaitForSeconds(0.5f);
        sceneTransition.Play("ANIM_FadeOut");
    }
}
