using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : CharacterAnimations
{
    
    [SerializeField]
    private Transform robotPiecesTrans;
    private List<SpriteRenderer> robotPieces;

    private void Awake()
    {
        robotPieces = new List<SpriteRenderer>();
        for(int i = 0; i < robotPiecesTrans.childCount; i++)
        {
            SpriteRenderer spr = robotPiecesTrans.GetChild(i).GetComponent<SpriteRenderer>();
            robotPieces.Add(spr);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        characterActions.OnGetRobotPieces += GetRobotPieces;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        characterActions.OnGetRobotPieces -= GetRobotPieces;
    }

    private List<SpriteRenderer> GetRobotPieces() 
    {
        return robotPieces;
    }

    protected override void FlipSprite(bool i)
    {
        float rot = (i) ? 0 : 180;

        robotPiecesTrans.rotation = Quaternion.Euler(robotPiecesTrans.rotation.x, rot, robotPiecesTrans.rotation.z);
    }
    protected override void SpawnAfterImage()
    {
        foreach (SpriteRenderer spr in robotPieces)
        {
            lastImagePos = transform.position;

            ObjectPooler.Instance.SpawnAfterImageFromPool("CharacterAfterImage", spr.transform.position, transform,
                Quaternion.identity, spr.sprite, spr.flipX);
        }
    }
}
