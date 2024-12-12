using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic state machine to handle transitions and execution of states.
/// </summary>
public class StateMachine<T>
{
    // Reference to the owner of the state machine.
    private T owner;

    // The current active state.
    private State<T> currentState;

    // The previous state (optional, for transitions back to the last state).
    private State<T> previousState;

    /// <summary>
    /// Constructor for the StateMachine.
    /// </summary>
    /// <param name="owner">The object that owns this state machine.</param>
    public StateMachine(T owner)
    {
        this.owner = owner;
    }
}

