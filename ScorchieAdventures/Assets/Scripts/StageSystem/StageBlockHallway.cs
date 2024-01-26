using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the triggering the transition between StageBlocks
*/

[RequireComponent(typeof(Collider2D))]
public class StageBlockHallway : MonoBehaviour
{
    private Collider2D col;

    [SerializeField] private bool isBack;

    [SerializeField] private StageBlock nextStageBlock;
    [SerializeField] private StageBlock previousStageBlock;
    [SerializeField] private StageBlockHallway secondaryHallway;

    public Transform previousPosition;
    public Transform nextPosition;

    private bool canCollide = true;
    public bool verticalTransition;

    [Space(10)]

    [Header("DayTime")]
    public bool changeDayTime;
    public DayTime dayTimeToUpdate;
    public DayTimeManager dayTimeManager;
    private bool changedTheDayTime = false;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StageBlocksHandler.OnStageBlockTransitionStarted += OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded += OnStageBlockTransitionEnded;
    }

    private void OnDisable()
    {
        StageBlocksHandler.OnStageBlockTransitionStarted -= OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded -= OnStageBlockTransitionEnded;
    }

    private void OnStageBlockTransitionStarted()
    {
        col.enabled = false;
    }

    private void OnStageBlockTransitionEnded()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && canCollide)
        {
            StageBlock newStageBlock = (isBack) ? previousStageBlock : nextStageBlock;

            StageBlocksHandler.previousPos = (isBack) ? nextPosition : previousPosition;
            StageBlocksHandler.nextPos = (isBack) ? previousPosition : nextPosition;

            StageBlocksHandler.OnStartBlocksTransition(newStageBlock);

            StageBlocksHandler.isVerticalTransition = verticalTransition;

            if (changeDayTime && !changedTheDayTime && dayTimeToUpdate != DayTimeManager.currentDayTime)
            {
                dayTimeManager.UpdateDayTime(dayTimeToUpdate, false);
                changedTheDayTime = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            canCollide = true;

            isBack = !isBack;
            if (secondaryHallway != null)
                secondaryHallway.isBack = !secondaryHallway.isBack;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(nextPosition.position, new Vector2(1f, 1f));
        Gizmos.DrawWireCube(previousPosition.position, new Vector2(1f, 1f));
    }
}
