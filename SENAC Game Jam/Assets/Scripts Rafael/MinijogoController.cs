using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MinijogoController : MonoBehaviour
{
    public GameObject textTutorial;

    public PlayableDirector playableDirector;
    public PlayableAsset winCutscene;
    public PlayableAsset loseCutscene;
    public bool needsCustscene;

    private void Start()
    {
        Invoke("CloseTutorialText", 4f);
    }

    void CloseTutorialText()
    {
        textTutorial.SetActive(false);
    }

    public void MinijogoEnded(bool result)
    {
        
        GameManager.instance.hasWonMinigame = result;
        Debug.Log("Has won: " + GameManager.instance.hasWonMinigame);

        if (!needsCustscene)
            ChangeToNewScene();


        if (result )
            playableDirector.playableAsset = winCutscene;
        else
            playableDirector.playableAsset = loseCutscene;

        playableDirector.Play();
    }

    public void ChangeToNewScene()
    {
        SceneManagement.instance.LoadLevel(SceneManagement.tabuleiroSceneIndex);
        Debug.Log("Acabou o minigame");
    }
}
