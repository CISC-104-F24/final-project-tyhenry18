using GDEUtils.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GameController class manages the game's state machine and transitions between different states.
/// </summary>
public class GameController : MonoBehaviour
{
    // Property to access the state machine instance.
    public StateMachine<GameController> StateMachine { get; private set; }

    /// <summary>
    /// Initializes the state machine and sets the starting state.
    /// </summary>
    private void Start()
    {
        // Initialize the state machine with this GameController as its owner.
        StateMachine = new StateMachine<GameController>(this);

        // Set the initial state to FreeRoamState.
        StateMachine.ChangeState(FreeRoamState.i);
    }

    /// <summary>
    /// Updates the active state every frame.
    /// </summary>
    private void Update()
    {
        // Execute the logic for the current state.
        StateMachine.Execute();
    }
}
