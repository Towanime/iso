using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    [Tooltip("Player avatar that will be rotated.")]
    public GameObject avatar;
    [Tooltip("Moving speed for the avatar.")]
    public float walkSpeed = 10f;
    [Tooltip("Position modifier when the player blinks.")]
    public float blinkDistance = 20f;
    [Tooltip("Rotation rate between directions.")]
    public float rotationSpeed = 300f;
    [Tooltip("Used to speed up the rotation when the new direction is opposite to the current one.")]
    public float rotationOppositeDirectionModifier = 2f;
    [Tooltip("TrailRenderer used when the player blinks.")]
    public TrailRenderer blinkTrailRenderer;
    private CharacterController characterController;
    // after blink or normal move calculations
    private float movementSpeed;
    // rotation variables
    private Quaternion targetRotation;
    private Vector3 currentDirection = Vector3.forward;
    private Vector3 nextDirection = Vector3.forward;
    private bool applyRotationModifier;
    // blink trail variables
    // initial time for the trail
    private float originalTrailDuration;

    void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        originalTrailDuration = blinkTrailRenderer.time;
        // set to 0 so blink can activate it later
        blinkTrailRenderer.time = 0;
    }

    void Update()
    {
        nextDirection = this.playerInput.direction;
        RotationUpdate();
        MovementSpeedUpdate();
        Vector3 direction = nextDirection * movementSpeed;

        this.characterController.Move(direction);
    }

    /// <summary>
    /// Modifies movemend speed if the blink is active.
    /// </summary>
    void MovementSpeedUpdate()
    {
        if (this.playerInput.blink)
        {
            // add blink speed and show trail!
            movementSpeed = walkSpeed * blinkDistance * Time.deltaTime;
            blinkTrailRenderer.time = originalTrailDuration;
        }
        else
        {
            // normal walk speed 
            movementSpeed = walkSpeed * Time.deltaTime;
            // reduce the trail time gradually to give it some ease
            if (blinkTrailRenderer.time >= 0)
            {
                blinkTrailRenderer.time = Mathf.Clamp(blinkTrailRenderer.time - Time.deltaTime, 0, originalTrailDuration);
            }
        }
    }

    /// <summary>
    /// Checks if the player avatar should face a new direction.
    /// </summary>
    void RotationUpdate()
    {
        // only start rotation if the player inputs a new direction
        if (currentDirection != nextDirection && nextDirection != Vector3.zero)
        {
            // check if the directions are opposite and need to rotate faster
            applyRotationModifier = currentDirection == -nextDirection;
            // set new rotation target
            currentDirection = nextDirection;
            targetRotation = Quaternion.LookRotation(currentDirection);
        }
        // rotate if needed
        if (avatar.transform.rotation != targetRotation)
        {
            avatar.transform.rotation = Quaternion.RotateTowards(avatar.transform.rotation, targetRotation,
                rotationSpeed * (applyRotationModifier ? rotationOppositeDirectionModifier : 1) * Time.deltaTime);
        }
    }
}