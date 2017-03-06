using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerState> {

    void Start()
    {
        currentState = PlayerState.Idle;
        Initialize();
    }
}
