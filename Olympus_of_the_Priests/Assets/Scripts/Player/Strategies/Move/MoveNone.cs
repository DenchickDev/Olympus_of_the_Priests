using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNone : IPlayerActionStrategy
{
    public void DoAction(Player player)
    {
        player.StopMovement();
    }
}
