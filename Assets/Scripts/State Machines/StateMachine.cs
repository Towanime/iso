using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour {
    /// <summary>
    /// Keep track of all the states for this kind of state machine.
    /// </summary>
    protected Dictionary<T, StateBehaviour<T>> states = new Dictionary<T, StateBehaviour<T>>();
    /// <summary>
    /// Active state for the state machine.
    /// </summary>
    protected T currentState;
    /// <summary>
    /// Used to avoid null pointer exeptions in some cases.
    /// </summary>
    protected bool initialized;

    /// <summary>
    /// Should be called by the child classes after setting the default state.
    /// </summary>
    protected void Initialize()
    {
        initialized = true;
        states[currentState].OnEnter();
    }

    public void RegisterState(T state, StateBehaviour<T> stateBehaviour)
    {
        states.Add(state, stateBehaviour);
    }

    public void ChangeState(T nextState)
    {
        // swap states and call their exit and enter methods
        StateBehaviour<T> current = states[currentState];
        StateBehaviour<T> next = states[nextState];
        currentState = nextState;
        if (initialized)
        {
            current.OnExit();
            next.OnEnter();
        }
    }
	
	// Update is called once per frame
	void Update () {
        states[currentState].OnUpdate();
    }

    void FixedUpdate()
    {
        states[currentState].OnFixedUpdate();
    }
}
