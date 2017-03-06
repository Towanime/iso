using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : StateBehaviour<PlayerState> {
    public PlayerStateMachine playerStateMachine;
    public PlayerMovement playerMovement;
    public Disk diskComponent;

    public override PlayerState GetState()
    {
        return PlayerState.DiskThrow;
    }

    public override StateMachine<PlayerState> GetStateMachine()
    {
        return playerStateMachine;
    }

    public override void OnEnter()
    {
        // disable movement and begin the throw
        //playerMovement.enabled = false;
        diskComponent.Throw();
    }

    public override void OnUpdate()
    {
        // check if the disk mechanic is done
        if(diskComponent.CurrentState == DiskStates.Default)
        {
            GetStateMachine().ChangeState(PlayerState.Idle);
        }
    }

    public override void OnExit()
    {
    }
}
