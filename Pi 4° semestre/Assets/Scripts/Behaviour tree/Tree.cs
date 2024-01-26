using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

    public abstract class Tree : MonoBehaviour
    {
        private NodeBehaviourtree _root = null;

        protected virtual void Start()
         {
            _root = SetupTree();
         }

        protected virtual void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        /// <summary>
        /// Creates the tree structure
        /// </summary>
        /// <returns></returns>
        protected abstract NodeBehaviourtree SetupTree();
    }
