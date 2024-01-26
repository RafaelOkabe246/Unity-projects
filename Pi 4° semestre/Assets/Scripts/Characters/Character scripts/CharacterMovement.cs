using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterActions))]
public class CharacterMovement : MonoBehaviour
{
    public Grid levelGrid;
    public Vector2 gridAttackTile;
    public Vector2 currentGridPosition;
    public BattleTile currentTile;
    public bool isLookingRight;
    public bool isMoving;
    public bool canMove;
    public CharacterActions characterActions;

    private void OnEnable()
    {
        characterActions.OnCheckCanMove += CheckCanMove;
        characterActions.OnMove += CallMove;
        characterActions.OnCheckPositionInGrid += CheckGridPosition;
        characterActions.OnCheckIsMoving += CheckIsMoving;
        characterActions.OnCheckLookingDirection += CheckIsLookingRight;
        characterActions.AttackLocation += CheckAttackArea;
        characterActions.OnCurrentTile += CheckCurrentTile;
        characterActions.OnCheckLeftTile += CheckLeftTile;
        characterActions.OnCheckRightTile += CheckRightTile;
        characterActions.OnFlip += Flip;
        characterActions.OnGetLevelGrid += GetLevelGrid;

    }

    private void OnDisable()
    {
        characterActions.OnCheckCanMove -= CheckCanMove;
        characterActions.OnMove -= CallMove;
        characterActions.OnCheckPositionInGrid -= CheckGridPosition;
        characterActions.OnCheckIsMoving -= CheckIsMoving;
        characterActions.OnCheckLookingDirection -= CheckIsLookingRight;
        characterActions.AttackLocation -= CheckAttackArea;
        characterActions.OnCurrentTile -= CheckCurrentTile;
        characterActions.OnCheckLeftTile -= CheckLeftTile;
        characterActions.OnCheckRightTile -= CheckRightTile;
        characterActions.OnFlip -= Flip;
        characterActions.OnGetLevelGrid -= GetLevelGrid;

    }


    public void SetInitialPosition()
    {
        currentTile = levelGrid.ReturnBattleTile(currentGridPosition);
        currentTile.OccupyTile(characterActions.OnCheckCharacter());

        transform.position = currentTile.transform.position;
    }

    private void Update()
    {
        if (!characterActions.OnCheckIsMoving() && !characterActions.OnCheckIsAttacking())
            canMove = true;
        else
            canMove = false;

        if (isLookingRight)
            gridAttackTile = currentGridPosition + Vector2.right;
        else
            gridAttackTile = currentGridPosition + Vector2.left;
    }

    #region FUNCS_METHODS
    bool CheckCanMove()
    {
        return canMove;
    }
    Grid GetLevelGrid()
    {
        return levelGrid;
    }
    BattleTile CheckRightTile()
    {
        return levelGrid.ReturnBattleTile(currentGridPosition + new Vector2(1, 0));
    }
    BattleTile CheckLeftTile()
    {
        return levelGrid.ReturnBattleTile(currentGridPosition - new Vector2(1, 0));
    }
    Vector2 CheckAttackArea()
    {
        return gridAttackTile;
    }

    Vector2 CheckGridPosition()
    {
        return currentGridPosition;
    }

    bool CheckIsMoving()
    {
        return isMoving;
    }

    bool CheckIsLookingRight()
    {
        return isLookingRight;
    }

    BattleTile CheckCurrentTile()
    {
        return currentTile;
    }

    #endregion


    void Flip()
    {
        isLookingRight = !isLookingRight;

        if(characterActions.OnCheckCharacter() is Player)
            characterActions.OnFlipSprite(!isLookingRight);
        else if (characterActions.OnCheckCharacter() is Enemy)
        {
            if (!isLookingRight)
                transform.localEulerAngles = new Vector3(0,0,0);
            else
                transform.localEulerAngles = new Vector3(0,180f, 0);
        }
    }

    public void InterruptMove()
    {
        StopAllCoroutines();
        transform.position = levelGrid.ReturnBattleTile(currentGridPosition).transform.position;
        SetMovement(false);
    }

    public void CallMove(Vector2 _direction)
    {
        if (characterActions.OnCheckCanMove())
          StartCoroutine(Move(_direction));
    }

    private void SetMovement(bool result) 
    {
        isMoving = result;
        characterActions.OnGetCharacterAnimations().isMoving = result;
    }

    private IEnumerator Move(Vector2 _direction)
    {

        Vector2 newGridPos = currentGridPosition + _direction;

        if ((isLookingRight && _direction.x < 0 && currentGridPosition.x != 0) || 
            (!isLookingRight && _direction.x > 0 && currentGridPosition.x != levelGrid.gridWidth - 1) ||
            (isLookingRight && _direction.x > 0 && currentGridPosition.x == levelGrid.gridWidth - 1))
        {
            Flip();
        }

        if (levelGrid.CanMoveGrid(newGridPos))
        {
            characterActions.OnGetCharacterAnimations().TriggerMoveAnimation();

            //Move to new Pos
            levelGrid.battleTiles.TryGetValue(newGridPos, out BattleTile _battleTile);

            if (_battleTile.GetOnMoveObject() == null && _battleTile.CompareStateWith(TileState.Empty)) //The tile is free to move 
            {
                SoundManager.instance.PlayAudio(AudiosReference.characterMove, AudioType.CHARACTER, null);
                _battleTile.onMoveObject = characterActions.OnCheckCharacter();
            }

            SetMovement(true);
            float elapsedTime = 0;

            currentGridPosition = _battleTile.gridPosition;
            currentTile.ClearTile();
            currentTile = levelGrid.ReturnBattleTile(currentGridPosition);
            _battleTile.OccupyTile(characterActions.OnCheckCharacter());

            while (elapsedTime < characterActions.GetMoveSpeed())
            {
                if (_battleTile.GetOnMoveObject() != characterActions.OnCheckCharacter()
                    || characterActions.OnCheckKnockdown())
                    InterruptMove();
                

                transform.position = Vector2.Lerp(transform.position, _battleTile.transform.position, (elapsedTime / characterActions.GetMoveSpeed()));
                elapsedTime += Time.deltaTime;

                

                yield return null;
            }
            if (_battleTile.GetOccupyingObject() != null
                 || _battleTile.GetOnMoveObject() != null && _battleTile.GetOnMoveObject() != this.gameObject)
                InterruptMove();

            _battleTile.onMoveObject = null;
            transform.position = levelGrid.ReturnBattleTile(currentGridPosition).transform.position;



            if (currentGridPosition.x == levelGrid.gridWidth -1 && isLookingRight
                || currentGridPosition.x == 0 && !isLookingRight)
            {
                Flip();
            }

            SetMovement(false);
        }

    }

}
