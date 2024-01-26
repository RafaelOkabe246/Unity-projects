using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : MonoBehaviour
{

    public IEnumerator Move(Transform newPos)
    {
        SoundSystem.instance.PlayAudio(AudioReference.footstep, AudioType.PLAYER);
        float timeElapsed = 0;
        float lerpDuration = 3;
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(transform.position, newPos.position, Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }   
}
