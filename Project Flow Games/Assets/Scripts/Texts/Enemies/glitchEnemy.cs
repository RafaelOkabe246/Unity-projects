using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitchEnemy : basicEnemy
{
    [SerializeField] float bombTime = 6f;
    [SerializeField] float currentBombTime;
    public Animator bombAnim;
    [SerializeField] bool hasExploded = false;
    [SerializeField] ParticleSystem debuffParticle;

    private void Start()
    {
        target = player.transform;
        currentBombTime = bombTime;
        StartCoroutine(BombCountDown());
    }

    private IEnumerator BombCountDown() {

        bombAnim.SetTrigger("Phase1");

        yield return new WaitForSeconds(bombTime / 3);

        bombAnim.SetTrigger("Phase2");

        yield return new WaitForSeconds(bombTime / 3);

        bombAnim.SetTrigger("Phase3");

        yield return new WaitForSeconds(bombTime / 3);
        debuffParticle.Play();
        bombAnim.SetTrigger("Explode");
        CallDebuff();
    }

    private void CallDebuff() {
        SoundSystem.instance.SearchSound(2,"");
        hasExploded = true;
        PostProcessingModifier.debuffActivated = true;
        PostProcessingModifier.EnableLSDEffects();
    }
}
