using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System.Linq;


/// <summary>
/// Beahaviour tree of a default enemy
/// </summary>
public class EnemyBT : Tree
{
    public BehaviourState currentBehaviourState;
    public EnemyStage currentEnemyStage;


    public CharacterActions characterActions;

    public Player player;

    public List<BattleTile> currentPath = new List<BattleTile>();
    public int pathIndex = 0;

    [Header("Battle tiles")]
    public BattleTile currentTile;
    public BattleTile targetTile;
    public BattleTile selectedTargetTile; //Current object targeting the tile


    [Header("Parameters")]
    public bool needsToChangePath;

    public float engageMoveDelay;
    public float followMoveDelay;
    public float moveDelay;

    public float attackDelay;
    [Range(0, 5)]
    public int agressivePoints; //Chance to engage 
    [Range(0, 10)]
    public int chanceToAttackRange; //Chance to attack range

    protected override void Start()
    {
        base.Start();
        needsToChangePath = true;
    }

    private void OnEnable()
    {
        characterActions.OnCheckBehaviourState += CheckBehaviourState;
        characterActions.OnChangeBehaviourState += SetBehaviourState;
    }

    private void OnDisable()
    {
        characterActions.OnCheckBehaviourState -= CheckBehaviourState;
        characterActions.OnChangeBehaviourState -= SetBehaviourState;
    }

    BehaviourState CheckBehaviourState()
    {
        return currentBehaviourState;
    }

    void SetBehaviourState(BehaviourState newBehaviourState)
    {
        if (currentBehaviourState == newBehaviourState)
            return;

        currentBehaviourState = newBehaviourState;
    }

    protected override NodeBehaviourtree SetupTree()
    {
        NodeBehaviourtree root = new Sequence(new List<NodeBehaviourtree>
        {

        });

        return root;
    }
}
