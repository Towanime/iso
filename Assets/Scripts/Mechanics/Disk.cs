﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    [Tooltip("Hand on the model from where the throw will begin.")]
    public GameObject anchor;
    /// <summary>
    /// Used for the throw calculations
    /// </summary>
    private GameObject target;
    [Tooltip("Used to see where in what direction the throw should go.")]
    public GameObject avatar;
    [Tooltip("Hidden disk to show when throwing but not during the animations.")]
    public GameObject disk;
    [Tooltip("How far in Z the disk will go.")]
    public float distance;
    [Tooltip("How fast the disk will go.")]
    public float speed;
    [Tooltip("How fast the disk will return.")]
    public float returnSpeed;
    public Animator animator;
    // starting throw time
    private Vector3 startPoint;
    private float startTime;
    private float journeyLength;
    private DiskStates currentState;
    // collider stuff
    private bool collided;
    private Collider diskCollider;
    private GameObject markedObject;

    void Start()
    {
        target = new GameObject();
        diskCollider = disk.GetComponent<Collider>();
    }

    void Update()
    {
        switch (currentState)
        {
            case DiskStates.Thrown:
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                // update position of the disk
                disk.transform.position = Vector3.Lerp(startPoint, target.transform.position, fracJourney);
                // got to the target?
                if (fracJourney >= 1)
                {
                    Comeback();
                }
                break;

            case DiskStates.Returning:
                distCovered = (Time.time - startTime) * returnSpeed;
                fracJourney = distCovered / journeyLength;
                disk.transform.position = Vector3.Lerp(startPoint, anchor.transform.position, fracJourney);
                // check destination
                if (fracJourney >= 1)
                {
                    // finish grab
                    End();
                }
                break;
        }
    }

    public void Throw()
    {
        // start throw
        currentState = DiskStates.Thrown;
        collided = false;
        markedObject = null;
        startTime = Time.time;
        // move target to the avatar forward
        Vector3 targetPosition = anchor.transform.position;
        targetPosition = targetPosition + (avatar.transform.forward.normalized * distance);
        target.transform.position = targetPosition;
        // set starting position and rotation for the real disk
        disk.transform.position = anchor.transform.position;
        disk.transform.rotation = anchor.transform.rotation;
        startPoint = anchor.transform.position;
        journeyLength = Vector3.Distance(anchor.transform.position, target.transform.position);
        // turn on collider
        diskCollider.enabled = true;
    }

    private void Comeback()
    {
        // start returning with delay
        currentState = DiskStates.Returning;
        startTime = Time.time;
        startPoint = disk.transform.position;
        journeyLength = Vector3.Distance(startPoint, anchor.transform.position);
    }

    private void End()
    {
        currentState = DiskStates.Default;
        startTime = 0;
        // renderers and collider
        diskCollider.enabled = false;
    }
    
    public void OnCollision(Collision collision)
    {
        collided = true;
        Debug.Log("Object collided: " + collision.gameObject.name);
        markedObject = collision.gameObject;
        Comeback();
    }

    public GameObject MarkedObject
    {
        get { return this.markedObject; }
    }

    public bool Collided
    {
        get { return this.collided; }
    }

    public DiskStates CurrentState
    {
        get { return this.currentState; }
    }
}
