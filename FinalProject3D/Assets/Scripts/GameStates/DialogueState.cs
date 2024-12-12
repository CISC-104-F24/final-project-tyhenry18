using GDEUtils.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : State<GameController>
{
    public static DialogueState i { get; private set; }
    private void Awake()
    {
        i = this;
    }

    public override void Enter(GameController owner)
    {
        Debug.Log("Entered Dialogue State");
    }

    public override void Execute()
    {
        Debug.Log("Executing Dialogue State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Dialogue State");
    }
}
