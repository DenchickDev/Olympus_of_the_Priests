using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonsPlayer
{
    private ActionButtons jump;
    private ActionButtons attack;
    private ActionButtons rollover;
    private ActionAxis move;

    public bool enableAllHard;

    public void SetEnableAllLite(bool val)
    {
        if (enableAllHard)
        {
            jump.enable = val;
            attack.enable = val;
            rollover.enable = val;
            move.enable = val;
        }
    }

    public ActionButtonsPlayer()
    {
        enableAllHard = true;
        jump = new ActionButtons(new KeyCode[] { KeyCode.Space });
        attack = new ActionButtons(new KeyCode[] { KeyCode.Mouse0 });
        rollover = new ActionButtons(new KeyCode[] { KeyCode.S });
        move = new ActionAxis("Horizontal");
    }

    public void SetEnableJump(bool val)
    {
        if (enableAllHard)
        {
            jump.enable = val;
        }
    }

    public bool CheckJump()
    {
        return enableAllHard ? jump.isKeyDownButtons() : false;
    }
    public bool isEnableJump()
    {
        return enableAllHard ? jump.enable : false;
    }

    public void SetEnableAattack(bool val)
    {
        if (enableAllHard)
        {
            attack.enable = val;
        }
    }

    public bool CheckAttack()
    {
        return enableAllHard ? attack.isKeyDownButtons() : false;
    }
    public bool isEnableAttack()
    {
        return enableAllHard ? attack.enable : false;
    }

    public void SetEnableRollover(bool val)
    {
        if (enableAllHard)
        {
            rollover.enable = val;
        }
    }

    public bool CheckRollover()
    {
        return enableAllHard ? rollover.isKeyDownButtons() : false;
    }
    public bool isEnableRollover()
    {
        return enableAllHard ? rollover.enable : false;
    }

    public void SetEnableMove(bool val)
    {
        if (enableAllHard)
        {
            move.enable = val;
        }
    }

    public bool CheckMove()
    {
        return enableAllHard ? move.isAxis() : false;
    }

    public float GetMove()
    {
        return enableAllHard ? move.GetAxis() : 0f;
    }
    public bool isEnableMove()
    {
        return enableAllHard ? move.enable : false;
    }
}
