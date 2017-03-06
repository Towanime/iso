using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implement all the logic depending that needs to be executed when depending on the state machine.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateBehaviour<T> : MonoBehaviour {
    /// <summary>
    /// Should be one of the different enums.
    /// </summary>
    /// <returns></returns>
    public abstract T GetState();

    public abstract StateMachine<T> GetStateMachine();

    void Start()
    {
        GetStateMachine().RegisterState(GetState(), this);
    }

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
