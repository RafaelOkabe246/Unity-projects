using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : CharacterAnimations
{
    private bool multiplied;
    private bool changedFlipX;

    [SerializeField]
    private GameObject attackFeedback; 

    private void LateUpdate()
    {
        if (characterActions.OnCheckIsAttacking())
        {
            if (!multiplied)
            {
                float scaleX = (gfx.flipX) ? -0.8f : 0.8f;
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
                if (scaleX < 0) 
                {
                    gfx.flipX = !gfx.flipX;
                    changedFlipX = true;
                    characterActions.GetLifeText().transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
                    attackFeedback.transform.localScale = new Vector3(-0.35f, attackFeedback.transform.localScale.y, attackFeedback.transform.localScale.z);
                }

                multiplied = true;
            }
        }
        else 
        {
            multiplied = false;
            transform.localScale = new Vector3(0.8f, transform.localScale.y, transform.localScale.z);
            if (changedFlipX) 
            {
                gfx.flipX = !gfx.flipX;
                changedFlipX = false;
                characterActions.GetLifeText().transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
                attackFeedback.transform.localScale = new Vector3(0.35f, attackFeedback.transform.localScale.y, attackFeedback.transform.localScale.z);
            }
        }
    }

    private void Update()
    {
        if (GameManager.instance.CurrentGameState == GameState.Paused)
            return;

        bool obsTarget = false;
        bool enTarget = false;


        BattleTile leftTile = characterActions.OnCheckLeftTile();
        if (leftTile) 
        {
            ObjectTile go = leftTile.GetOccupyingObject();

            if (go) 
            {
                obsTarget = (go.GetComponent<DestroyableObject>() != null);
                enTarget = (go.GetComponent<Enemy>() != null);
            }
        }

        BattleTile rightTile = characterActions.OnCheckRightTile();
        if (rightTile) 
        {
            ObjectTile go = rightTile.GetOccupyingObject();

            if (go) 
            {
                if (!obsTarget)
                    obsTarget = (go.GetComponent<DestroyableObject>() != null);
                if (!enTarget)
                    enTarget = (go.GetComponent<Enemy>() != null);
            }
        }

        if (obsTarget || enTarget)
        {
            attackFeedback.SetActive(true);
        }
        else 
        {
            attackFeedback.SetActive(false);
        }
    }
}
