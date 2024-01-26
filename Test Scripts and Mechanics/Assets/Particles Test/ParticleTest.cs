using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public enum Particles
    {
        Blood,
        Sparks
    }
    public Particles currentParticle;


    public int timesLoop;
    public GameObject bloodParticles, sparkParticles;
    public GameObject currentParticleObj;
    public Transform spawnPoint;

    private void Start()
    {
        SelectParticle();
        for (int i = 0; i < timesLoop; i++)
        {
            
        }
        Instantiate(currentParticleObj, spawnPoint.position, Quaternion.identity);
    }

    void SelectParticle()
    {
        switch (currentParticle)
        {
            case (Particles.Blood):
                currentParticleObj = bloodParticles;
                break;
            case (Particles.Sparks):
                currentParticleObj = sparkParticles;
                break;
        }
    }

}
