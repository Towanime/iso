using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject avatar;
    public float walkSpeed = 3f;
    public float rotationSpeed = 10f;
    private float currentRotationVelocity = 0.0f;
    private Vector3 currentRotationVelocityV = Vector3.zero;
    private CharacterController characterController;
    private float initialY;
    private Camera camera;

    void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.camera = Camera.main;
    }

    void Update()
    {
        Vector3 newDir = Vector3.RotateTowards(avatar.transform.forward, this.playerInput.direction, rotationSpeed, 0.0f);
        avatar.transform.rotation = Quaternion.LookRotation(newDir);
        Vector3 direction = this.playerInput.direction * walkSpeed * Time.deltaTime;

        this.characterController.Move(direction);
    }
}