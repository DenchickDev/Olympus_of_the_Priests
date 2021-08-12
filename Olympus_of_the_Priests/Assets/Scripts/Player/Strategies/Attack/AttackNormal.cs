using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNormal : IPlayerActionStrategy
{
    public void DoAction(Player player)
    {
        player.state = Player.State.Stab;
        player.soundManager.PlayHitSound();
    }
}