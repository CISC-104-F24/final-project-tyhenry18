using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDEUtils.StateMachine
{
    /// <summary>
    /// Base class for implementing states in a state machine.
    /// </summary>
    /// <typeparam name="T">The type of the owner object this state will operate on.</typeparam>
    public class State<T> : MonoBehaviour
    {
        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        /// <param name="owner">The object owning the state machine.</param>
        public virtual void Enter(T owner) { }

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public virtual void Exit() { }
    }
}
