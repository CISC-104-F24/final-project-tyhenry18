using GDEUtils.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamState : State<GameController>
{
    public static FreeRoamState i { get; private set; }
    private void Awake()
    {
        i = this;
    }

    GameController gc;
    public override void Enter(GameController owner)
    {
        gc = owner;

        Debug.Log("Entered FreeRoam State");
    }

    public override void Execute()
    {
        Debug.Log("Executing FreeRoam State");

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gc.StateMachine.ChangeState(DialogueState.i);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting FreeRoam State");
    }
}
