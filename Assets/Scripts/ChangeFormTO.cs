using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFormTO : BaseChangeForm
{
    // Start is called before the first frame update
    void Start()
    {
        initPos.v1 = Vector3.zero;
        initPos.v2 = Vector3.right;
        initPos.v3 = Vector3.up;
        initPos.v4 = Vector3.up + Vector3.right;

        Brick.State state1;
        state1.v1 = new Vector3(0, 0, 0);
        state1.v2 = new Vector3(0, 0, 0);
        state1.v3 = new Vector3(0, 0, 0);
        state1.v4 = new Vector3(0, 0, 0);
        form.Add(state1);
    }
}