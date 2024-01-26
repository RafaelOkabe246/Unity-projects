using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public enum UiAnimationTypes
{
    Move,
    Scale,
    Rotate,
    Fade
}


public class UiAnimationHandler : MonoBehaviour
{
    private GameObject animateObject;

    public UiAnimationTypes animationType;
    public Ease easeType;
    public LoopType loopType;

    public float duration;
    public float delay;

    private int loopCount;
    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;

    public bool pingPongEffect;
    public bool loop;
    public bool showOnEnable;


    private void OnEnable()
    {
        if (showOnEnable)
            Show();
    }

    public void Show()
    {
        HandleTween();
    }

    private void HandleTween()
    {
        if (animateObject == null)
            animateObject = this.gameObject;

        if (loop)
        {
            loopCount = -1;
        }
        else
        {
            loopCount = 0;
        }

        switch (animationType)
        {
            case UiAnimationTypes.Move:
                Move();
                break;
            case UiAnimationTypes.Scale:
                Scale();
                break;
            case UiAnimationTypes.Fade:
                Fade();
                break;
            case UiAnimationTypes.Rotate:
                Rotate();
                break;
        }


    }

    #region Tween_methods
    private void Rotate()
    {
        if (animateObject.GetComponent<RectTransform>() == null)
            animateObject.AddComponent<RectTransform>();

        RectTransform rectTransform = animateObject.GetComponent<RectTransform>();

        rectTransform.DOLocalRotate( to, duration).SetEase(easeType).SetLoops(loopCount,loopType);
    }

    private void Scale()
    {
        if (animateObject.GetComponent<RectTransform>() == null)
            animateObject.AddComponent<RectTransform>();

        RectTransform rectTransform = animateObject.GetComponent<RectTransform>();

        if (startPositionOffset)
            rectTransform.anchoredPosition = from;

        rectTransform.DOScale(to, duration).SetEase(easeType).SetLoops(loopCount, loopType); 
    }

    private void Move()
    {
        if (animateObject.GetComponent<RectTransform>() == null)
            animateObject.AddComponent<RectTransform>();

        RectTransform rectTransform = animateObject.GetComponent<RectTransform>();

        if (startPositionOffset)
            rectTransform.anchoredPosition = from;

        rectTransform.DOLocalMove(to, duration).SetEase(easeType).SetLoops(loopCount, loopType); 
    }

    private void Fade()
    {
        if(animateObject.GetComponent<CanvasGroup>() == null)
        {
            animateObject.AddComponent<CanvasGroup>();
        }

        CanvasGroup canvasGroup = animateObject.GetComponent<CanvasGroup>();

        if (startPositionOffset)
            canvasGroup.alpha = from.x;

        canvasGroup.DOFade(to.x, duration).SetEase(easeType).SetLoops(loopCount, loopType);
    }
    #endregion

    public void OnClose()
    {
        OnComplete();
    }

    async void OnComplete()
    {

        SwapDirection();

        HandleTween();

        await Task.Delay((int)duration * 1000);
        this.gameObject.SetActive(false);

        SwapDirection();

    }

    private void SwapDirection()
    {
        var temp = from;
        from = to;
        to = temp;
    }
}