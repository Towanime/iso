﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("Transform to use to see if there is ground collision.")]
    public Transform groundCheckObject;
    [Tooltip("Ground layer to check.")]
    public LayerMask groundLayer;
    [Tooltip("Y limit when falling to the void.")]
    public float yVoidLimit;
    public float groundRadius = 0.2f;
    private bool grounded = false;
    private bool calculated = false;

    private void Update()
    {
        calculated = false;
    }

    private void FixedUpdate()
    {
        calculated = false;
    }

    private void LateUpdate()
    {
        // verify if the y limit has been reached and kill the player
        if(transform.position.y <= yVoidLimit)
        {
            GetComponent<DamageableEntity>().OnDamage(gameObject, 9999);
        }
    }

    public bool IsGrounded
    {
        get
        {
            if (!calculated)
            {
                grounded = Physics.CheckSphere(groundCheckObject.position, groundRadius, groundLayer);
                calculated = true;
            }
            return grounded;
        }
    }
}