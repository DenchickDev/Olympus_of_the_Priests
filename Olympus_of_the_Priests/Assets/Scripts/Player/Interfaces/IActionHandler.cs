using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionHandler
{
    void SetStrategy(IPlayerActionStrategy s);
    bool DoActionIfYouCan(Player player);
}
