using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : CharacterCollision
{
    private List<Material> robotPiecesMaterials;

    protected override void Start()
    {
        coll = GetComponent<Collider2D>();
        character = GetComponent<Character>();

        robotPiecesMaterials = new List<Material>();
        foreach (SpriteRenderer spr in characterActions.OnGetRobotPieces())
        {
            robotPiecesMaterials.Add(spr.material);
        }
    }

    protected override IEnumerator DamageFlasher()
    {
        foreach (Material mat in robotPiecesMaterials)
        {
            SetFlashColor(mat);
        }

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
            foreach (Material mat in robotPiecesMaterials)
            {
                SetFlashAmount(currentFlashAmount, mat);
            }

            yield return null;
        }
    }
}
