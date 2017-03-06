using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehavior : StateBehavior<PlayerState> {

    public override PlayerState GetState()
    {
        return PlayerState.DiskThrow;
    }

    
}
