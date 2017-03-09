using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : StateBehaviour<PlayerState> {
    public PlayerStateMachine playerStateMachine;
    public PlayerMovement playerMovement;
    public GameObject pulseBombPrefab;
    public float forceMultiplier;
    public GameObject throwAnchor;
    public Transform avatarTransform;

    public override PlayerState GetState()
    {
        return PlayerState.PulseBomb;
    }

    public override StateMachine<PlayerState> GetStateMachine()
    {
        return playerStateMachine;
    }

    public override void OnEnter()
    {
        // disable movement and begin the throw
        playerMovement.enabled = false;
    }

    public override void OnUpdate()
    {
        // check if the disk mechanic is done
        GameObject pulseBomb = Instantiate(pulseBombPrefab, throwAnchor.transform.position, Quaternion.identity);
        Rigidbody rb = pulseBomb.GetComponent<Rigidbody>();
        Vector3 direction = throwAnchor.transform.forward;
        direction.y = 0.1f;
        rb.AddForce(direction * forceMultiplier, ForceMode.Impulse);
        // back to idle
        playerStateMachine.ChangeState(PlayerState.Idle);
    }

    public override void OnExit()
    {
    }
}
