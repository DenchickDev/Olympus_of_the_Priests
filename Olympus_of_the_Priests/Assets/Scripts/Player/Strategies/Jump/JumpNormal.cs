using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNormal : IPlayerActionStrategy
{
    public void DoAction(Player player)
    {
        player.Jump();
    }
}
