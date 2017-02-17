using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public float walkSpeed = 3f;
    private CharacterController characterController;
    private Camera camera;

    void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.camera = Camera.main;
    }

    void Update()
    {
        Vector3 direction = this.playerInput.direction * walkSpeed * Time.deltaTime;

        this.characterController.Move(direction);
        this.transform.rotation *= Quaternion.Euler(0f, this.playerInput.rotation.x, 0f);
        this.camera.transform.rotation *= Quaternion.Euler(this.playerInput.rotation.y, 0f, 0f);
    }
}