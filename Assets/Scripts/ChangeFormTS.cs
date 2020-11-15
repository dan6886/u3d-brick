using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFormTS : BaseChangeForm
{
    // Start is called before the first frame update
    void Start()
    {
        Brick.State state1;
        state1.v1 = 2 * Vector3.right;
        state1.v2 = Vector3.up + Vector3.right;
        state1.v3 = Vector3.zero;
        state1.v4 = Vector3.up + Vector3.left;
        form.Add(state1);
        Brick.State state2;
        state2.v1 = 2 * Vector3.left;
        state2.v2 = Vector3.down + Vector3.left;
        state2.v3 = Vector3.zero;
        state2.v4 = Vector3.right + Vector3.down;
        form.Add(state2);
    }
}