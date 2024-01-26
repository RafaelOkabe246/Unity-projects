using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GenericObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gfx;
    public ObjectActions objectActions = new ObjectActions();
    private Animator anim;
    public TestInteractionsScrptableObj testScripObj;
    


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        objectActions.OnGetAnimator += GetAnimator;
        objectActions.OnGetSpriteRenderer += GetSpriteRenderer;
    }

    private void OnDisable()
    {
        objectActions.OnGetAnimator -= GetAnimator;
        objectActions.OnGetSpriteRenderer -= GetSpriteRenderer;
    }

    Animator GetAnimator()
    {
        return anim;
    }

    SpriteRenderer GetSpriteRenderer()
    {
        return gfx;
    }
}