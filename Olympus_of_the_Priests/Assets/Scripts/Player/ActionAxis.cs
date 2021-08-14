using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAxis
{
    public bool enable;
    string axisName;

    public ActionAxis(string _axisName)
    {
        axisName = _axisName;
        enable = true;
    }

    public bool isAxis()
    {
        if (enable)
        {
            return Input.GetAxis(axisName) != 0;
        }
        return false;
    }

    public float GetAxis()
    {
        if (enable)
        {
            return Input.GetAxis(axisName);
        }
        return 0f;
    }
}
