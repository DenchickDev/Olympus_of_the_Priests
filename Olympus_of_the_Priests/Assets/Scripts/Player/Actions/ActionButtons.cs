using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : IActionHandler
{
    private KeyCode[] buttons;
    private IPlayerActionStrategy strategy;

    public ActionButtons(KeyCode[] _buttons, IPlayerActionStrategy s = null)
    {
        strategy = s;
        buttons = _buttons;
    }

    public bool isKeyDownButtons()
    {
        foreach (var button in buttons)
        {
            if (Input.GetKeyDown(button))
            {
                return true;
            }
        }
        return false;
    }

    public bool isKeyUpButtons()
    {
        foreach (var button in buttons)
        {
            if (Input.GetKeyUp(button))
            {
                return true;
            }
        }
        return false;
    }

    public bool DoActionIfYouCan(Player player)
    {
        if (isKeyDownButtons())
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
