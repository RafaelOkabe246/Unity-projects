using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : NodeBehaviourtree
{
    public Sequence() : base() { }
    public Sequence(List<NodeBehaviourtree> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach (NodeBehaviourtree node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    anyChildIsRunning = true;
                    continue;
                default:
                    state = NodeState.SUCCESS;
                    return state;
            }
        }

        state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return state;
    } 
}
