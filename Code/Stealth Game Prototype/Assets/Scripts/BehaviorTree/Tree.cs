using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tutorial from: https://www.youtube.com/watch?v=aR6wt5BlE-E&ab_channel=MinaP%C3%AAcheux
// This file was NOT written by me

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract Node SetupTree();

    }

}
