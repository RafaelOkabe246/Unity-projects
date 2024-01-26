using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningFloor : WallObstacle
{
    public string BurningAudio = "Burning Audio";
    
    protected override void OnValidate()
    {
        col = GetComponent<BoxCollider2D>();
        
        if (!isHorizontal)
        {
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
        }
        gfx.size = new Vector2(lenghtValue, 1);
        col.size = new Vector2(lenghtValue, 0.6f);
    }

    protected override void OnEnable()
    {
        isDestroyable = true;
        isHorizontal = true;
        startActive = true;
        base.OnEnable();
    }

    public override void TriggerEvent()
    {
        if (isActivate)
            isActivate = false;
        else
            isActivate = true;

        Event();
    }

    protected override void ChangeTagAndLayer()
    {
        if (isActivate)
        {
            this.gameObject.layer = groundLayer;
            transform.gameObject.tag = tagCollidePlayer;
        }
        else if (!isActivate)
        {
            this.gameObject.layer = ignoreLayer;
            transform.gameObject.tag = tagIgnorePlayer;
        }
    }

    IEnumerator OnTriggerBurn()
    {
        isActivate = false;
        yield return new WaitForSeconds(1f);
        this.transform.gameObject.layer = ignoreLayer;
        this.transform.gameObject.tag = tagIgnorePlayer;
        StartCoroutine(OnTriggerRestore());
    }

    public override void Event()
    {
        StartCoroutine(OnTriggerBurn());
    }

    IEnumerator OnTriggerRestore()
    {
        yield return new WaitForSeconds(1.5f);
        isActivate = true;
        this.transform.gameObject.layer = groundLayer;
        this.transform.gameObject.tag = tagCollidePlayer;

    }
}
