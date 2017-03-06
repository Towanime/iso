using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour {
    /// <summary>
    /// Keep track of all the states for this kind of state machine.
    /// </summary>
    private Dictionary<T, StateBehavior<T>> states = new Dictionary<T, StateBehavior<T>>();
    /// <summary>
    /// Active state for the state machine.
    /// </summary>
    private T currentState;
    /// <summary>
    /// Used to avoid null pointer exeptions in some cases.
    /// </summary>
    private bool initialized;

    /// <summary>
    /// Should be called by the child classes after setting the default state.
    /// </summary>
    private void Initialize()
    {
        initialized = true;
        states[currentState].OnEnter();
    }

    public void ChangeState(T nextState)
    {
        // swap states and call their exit and enter methods
        StateBehavior<T> current = states[currentState];
        StateBehavior<T> next = states[nextState];
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
