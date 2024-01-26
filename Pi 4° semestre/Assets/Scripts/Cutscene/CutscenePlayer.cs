using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField]
    private float delayToPlayCutscene;
    [SerializeField]
    private float delayToStartGame;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene() 
    {
        yield return new WaitForSeconds(delayToPlayCutscene);

        videoPlayer.Play();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame() 
    {
        yield return new WaitForSeconds(delayToStartGame);

        SceneLoader.instance.LoadNextLevel();
    }
}
