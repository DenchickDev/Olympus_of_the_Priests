using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolloverNormal : IPlayerActionStrategy
{
    public void DoAction(Player player)
    {
        player.OnRollover();
    }
}
