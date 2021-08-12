using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action : ulong
{
    Nothing = 0,
    Jump = 1 << 0,
    Attack = 1 << 1,
    Rollover = 1 << 2,
    Move = 1 << 3,
    All = 0xFFFFFFFFFFFFFFFF
}

public class ActionButtonsPlayer
{
    private Dictionary<Action, IActionHandler> actions = new Dictionary<Action, IActionHandler>();
    private static readonly Dictionary<Action, IPlayerActionStrategy> defaultStrategies = new Dictionary<Action, IPlayerActionStrategy>
    {
        { Action.Jump , new JumpNormal() },
        { Action.Attack , new AttackNormal() },
        { Action.Rollover , new RolloverNormal() },
        { Action.Move , new MoveNormal() },
    };

    public bool enableAllHard { private get; set; } = true;

    public void SetEnableAllLite(bool val)
    {
        if (enableAllHard)
        {
            if (val)
            {
                ResetActions();
            }
            else
            {
                DisableAll();
            }
        }
    }

    public void Enable(Action action)
    {
        if (!enableAllHard)
        {
            return;
        }

        foreach (var act in actions)
        {
            if ((act.Key & action) != 0)
            {
                SetActionDefaultStrategy(act.Key & action);
            }
        }
    }

    public void Disable(Action action)
    {
        if (!enableAllHard)
        {
            return;
        }

        foreach (var act in actions)
        {
            if ((act.Key & action) != 0)
            {
                SetActionVoidStrategy(act.Key & action);
            }
        }
    }

    public ActionButtonsPlayer()
    {
        actions.Add(Action.Jump, new ActionButtons(new[] { KeyCode.Space }));
        actions.Add(Action.Attack, new ActionButtons(new[] { KeyCode.Mouse0 }));
        actions.Add(Action.Rollover, new ActionButtons(new[] { KeyCode.S }));
        actions.Add(Action.Move, new ActionAxis("Horizontal"));
        ResetActions();
    }

    public void ResetActions()
    {
        if (!enableAllHard)
        {
            return;
        }

        foreach (var defStrategy in defaultStrategies)
        {
            actions[defStrategy.Key].SetStrategy(defStrategy.Value);
        }
    }

    private void SetActionDefaultStrategy(Action action)
    {
        actions[action].SetStrategy(defaultStrategies[action]);//.Value);
    }

    private void SetActionVoidStrategy(Action action)
    {
        actions[action].SetStrategy(new VoidAction());
    }

    public void DisableAll()
    {
        if (!enableAllHard)
        {
            return;
        }

        foreach (var action in actions)
        {
            if (action.Key == Action.Move)
            {
                action.Value.SetStrategy(new MoveNone());
            }
            else
            {
                action.Value.SetStrategy(new VoidAction());
            }
        }
    }

    public void DoActionsIfKeyPressed(Player player)
    {
        foreach (var action in actions)
        {
            if (action.Value.DoActionIfYouCan(player))
            {
                break;
            }
        }
    }

    public void SetDrunkard()
    {
        if (!enableAllHard)
        {
            return;
        }

        actions[Action.Jump].SetStrategy(new RolloverNormal());
        actions[Action.Rollover].SetStrategy(new JumpNormal());
    }
}
