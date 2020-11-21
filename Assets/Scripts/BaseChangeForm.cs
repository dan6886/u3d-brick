using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChangeForm : MonoBehaviour
{
    protected ArrayList form = new ArrayList();
    private int index = 0;
    protected Brick.State initPos;

    public Brick.State nextState()
    {
        var newIndex = (form.Count + index) % (form.Count);
        index++;
        return (Brick.State) form[newIndex];
    }

    public void rollBackState()
    {
        index--;
    }

    public Brick.State getInitPos()
    {
        return initPos;
    }
}