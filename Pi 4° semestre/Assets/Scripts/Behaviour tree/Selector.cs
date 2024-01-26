using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : NodeBehaviourtree
{
    public Selector() : base() { }
    public Selector(List<NodeBehaviourtree> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (NodeBehaviourtree node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.SUCCESS;
                    return state;
                default:
                    continue;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
