using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNormal : IPlayerActionStrategy
{
    public void DoAction(Player player)
    {
        player.Movement();
    }
}
