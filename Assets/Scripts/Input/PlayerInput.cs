using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Current key configuration.")]
    public KeyboardMouseConfig config;
    [Tooltip("Direction to where the player will move next.")]
    public Vector3 direction;
    [Tooltip("Rotation from the mouse to apply on the camera.")]
    public Vector3 rotation;
    /// <summary>
    /// True if the player pressed the action key in this frame.
    /// </summary>
    public bool action;
    /// <summary>
    /// True if the player is holding the run key.
    /// </summary>
    public bool run;

    void Update()
    {
        // update values depending on the input
        this.SetDirection();
        this.SetRotation();
        this.SetAction();
        this.SetRun();
    }

    private void SetDirection()
    {
        this.direction = Vector3.zero;

        if (Input.GetKey(this.config.forward))
        {
            this.direction += Vector3.forward;
        }
        else if (Input.GetKey(this.config.backwards))
        {
            this.direction += Vector3.back;
        }

        if (Input.GetKey(this.config.left))
        {
            this.direction += Vector3.left;
        }
        else if (Input.GetKey(this.config.right))
        {
            this.direction += Vector3.right;
        }

        this.direction = this.direction.normalized;
    }

    private void SetRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * this.config.mouseXSensitivity;
        float pitch = Input.GetAxis("Mouse Y") * this.config.mouseYSensitivity;

        if (this.config.invertY)
        {
            pitch *= -1;
        }

        this.rotation = new Vector3(yaw, pitch, 0f);
    }

    private void SetAction()
    {
        this.action = Input.GetKeyDown(this.config.action);
    }

    private void SetRun()
    {
        this.run = Input.GetKey(this.config.run);
    }
}
