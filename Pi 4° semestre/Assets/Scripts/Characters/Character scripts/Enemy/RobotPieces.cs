using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPieces : MonoBehaviour
{
    public SpriteRenderer AntennaGFX;
    public Sprite AntennaSprite;
    [Space(1)]
    public SpriteRenderer HeadGFX;
    public Sprite HeadSprite;
    [Space(1)]
    public SpriteRenderer EyesGFX;
    public Sprite EyeSprite;
    [Space(1)]
    public SpriteRenderer BodyGFX;
    public Sprite BodySprite;
    [Space(1)]
    public SpriteRenderer RightArmGFX;
    public Sprite RightArmSprite;
    [Space(1)]
    public SpriteRenderer LeftArmGFX;
    public Sprite LeftArmSprite;
    [Space(1)]
    public SpriteRenderer FeetGFX;
    public Sprite FeetSprite;

    public List<SpriteRenderer> bodyPieces;

    private void Start()
    {
        foreach (SpriteRenderer bodyPiece in GetComponentsInChildren<SpriteRenderer>())
        {
            bodyPieces.Add(bodyPiece);
        }

        AntennaGFX.sprite = AntennaSprite;
        HeadGFX.sprite = HeadSprite;
        EyesGFX.sprite = EyeSprite;
        BodyGFX.sprite = BodySprite;
        RightArmGFX.sprite = RightArmSprite;
        LeftArmGFX.sprite = LeftArmSprite;
        FeetGFX.sprite = FeetSprite;
    }
}
