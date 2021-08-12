using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAxis : IActionHandler
{
    string axisName;
    private IPlayerActionStrategy strategy;

    public ActionAxis(string _axisName)
    {
        axisName = _axisName;
    }

    public bool isAxis()
    {
        return Input.GetAxis(axisName) != 0;
    }



    public bool DoActionIfYouCan(Player player)
    {
        if (isAxis())
        {
            strategy.DoAction(player);
            return true;
        }
        return false;
    }

    public void SetStrategy(IPlayerActionStrategy s)
    {
        strategy = s;
    }
}
