using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons
{
    public bool enable;
    KeyCode[] buttons;

    public ActionButtons(KeyCode[] _buttons)
    {
        enable = true;
        buttons = _buttons;
    }

    public bool isKeyDownButtons()
    {
        if (enable)
        {
            foreach (var button in buttons)
            {
                if (Input.GetKeyDown(button))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool isKeyUpButtons()
    {
        if (enable)
        {
            foreach (var button in buttons)
            {
                if (Input.GetKeyUp(button))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
