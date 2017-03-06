using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateBehaviour : StateBehaviour<PlayerState> {
    public PlayerStateMachine playerStateMachine;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    public override PlayerState GetState()
    {
        return PlayerState.Idle;
    }

    public override StateMachine<PlayerState> GetStateMachine()
    {
        return playerStateMachine;
    }

    public override void OnEnter()
    {
        // restore player movement from any state
        playerMovement.enabled = true;
    }

    public override void OnUpdate()
    {
        if (playerInput.action)
        {
            playerStateMachine.ChangeState(PlayerState.DiskThrow);
        }
    }
}
