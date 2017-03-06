using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implement all the logic depending that needs to be executed when depending on the state machine.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateBehavior<T> : MonoBehaviour {
    /// <summary>
    /// Main state machine where this state is used.
    /// </summary>
    private StateMachine<T> stateMachine;
    /// <summary>
    /// Should be one of the different enums.
    /// </summary>
    /// <returns></returns>
    public abstract T GetState();

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
