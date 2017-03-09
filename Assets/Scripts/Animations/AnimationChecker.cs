using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChecker : MonoBehaviour {
    public Animator animator;
    public PlayerInput playerInput;

	// Update is called once per frame
	void Update () {
        animator.SetBool("IsRunning", playerInput.direction != Vector3.zero);
        animator.SetBool("Blink", playerInput.blink);
    }
}
