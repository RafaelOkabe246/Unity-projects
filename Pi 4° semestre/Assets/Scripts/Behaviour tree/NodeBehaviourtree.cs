using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class NodeBehaviourtree 
    {
        protected NodeState state;

        public NodeBehaviourtree parent;
        protected List<NodeBehaviourtree> children = new List<NodeBehaviourtree>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public NodeBehaviourtree()
        {
            parent = null;
        }
        public NodeBehaviourtree(List<NodeBehaviourtree> children)
        {
            foreach (NodeBehaviourtree child in children)
                _Attach(child);
        }
        private void _Attach(NodeBehaviourtree node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            NodeBehaviourtree node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            NodeBehaviourtree node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}

