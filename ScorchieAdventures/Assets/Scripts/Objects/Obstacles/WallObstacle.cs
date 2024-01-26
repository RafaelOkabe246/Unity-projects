using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : Obstacle
{
    public bool startActive;
    [SerializeField] 
    protected bool isDestroyable;
    public Color normalColor;

    [Space(5f)]

    [SerializeField] 
    protected bool isHorizontal;
    [SerializeField] 
    protected BoxCollider2D col;
    public float lenghtValue;

    protected float dissolveAmount;

    protected virtual void OnValidate()
    {
        col = GetComponent<BoxCollider2D>();

        if (isHorizontal)
        {
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
        }
        gfx.size = new Vector2(1, lenghtValue);
        col.size = new Vector2(0.5f, lenghtValue);
    }

    private void Start()
    {
        CheckActive();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckActive();
    }

    protected void Update()
    {
        if (isActivate)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
        }
        gfx.material.SetFloat("_DissolveAmount", dissolveAmount);
    }

    protected virtual void CheckActive()
    {
        isActivate = startActive;
        ChangeTagAndLayer();

    }

    public void TriggerEventByCollision()
    {
        if (!isDestroyable)
            return;

        TriggerEvent();
    }

    public override void TriggerEvent()
    {
       if (isActivate)
            isActivate = false;
        else
            isActivate = true;
        
        Event();
    }

    public override void Event()
    {
        ChangeTagAndLayer();
    }

    protected virtual void ChangeTagAndLayer()
    {
        if (isActivate)
        {
            if (isDestroyable)
            {
                this.gameObject.layer = groundLayer;
                transform.gameObject.tag = tagCollidePlayer;
            }
            else
            {
                this.gameObject.layer = groundLayer;
                transform.gameObject.tag = tagIgnorePlayer;
            }

        }
        else if (!isActivate)
        {
            this.gameObject.layer = ignoreLayer;
            transform.gameObject.tag = tagIgnorePlayer;
        }
        
    }

}
