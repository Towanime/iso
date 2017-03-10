using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToActivator : BaseActivator
{
    public GameObject toMove;
    public Transform desiredPosition;
    public float speed;

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 nextPosition = Vector3.MoveTowards(toMove.transform.position, desiredPosition.transform.position, speed * Time.fixedDeltaTime);
            toMove.transform.Translate(nextPosition - toMove.transform.position);
        }
        // turns off until the trigger activates it again.
        active = false;
    }
}
