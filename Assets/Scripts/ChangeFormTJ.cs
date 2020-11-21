using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFormTJ : BaseChangeForm
{
    // Start is called before the first frame update
    void Start()
    {
        initPos.v1 = Vector3.left;
        initPos.v2 = Vector3.zero;
        initPos.v3 = Vector3.up;
        initPos.v4 = 2 * Vector3.up;

        Brick.State state1;
        state1.v1 = 2 * Vector3.up;
        state1.v2 = Vector3.left + Vector3.up;
        state1.v3 = Vector3.zero;
        state1.v4 = Vector3.down + Vector3.right;
        form.Add(state1);
        Brick.State state2;
        state2.v1 = 2 * Vector3.right;
        state2.v2 = Vector3.up + Vector3.right;
        state2.v3 = Vector3.zero;
        state2.v4 = Vector3.left + Vector3.down;
        form.Add(state2);
        Brick.State state3;
        state3.v1 = 2 * Vector3.down;
        state3.v2 = Vector3.down + Vector3.right;
        state3.v3 = Vector3.zero;
        state3.v4 = Vector3.left + Vector3.up;
        form.Add(state3);
        Brick.State state4;
        state4.v1 = 2 * Vector3.left;
        state4.v2 = Vector3.down + Vector3.left;
        state4.v3 = Vector3.zero;
        state4.v4 = Vector3.right + Vector3.up;
        form.Add(state4);
    }
}