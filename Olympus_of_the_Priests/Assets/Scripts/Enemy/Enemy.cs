using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Тип патруля
    /// </summary>
    public enum TypePatrol
    {
        /// <summary>
        /// Земной
        /// </summary>
        Ground,
        /// <summary>
        /// Воздушный
        /// </summary>
        Air,
        /// <summary>
        /// Не петрулирует
        /// </summary>
        NoPatrol
    }

    /// <summary>
    /// Текущее состояние
    /// </summary>
    public TypePatrol typePatrol = TypePatrol.NoPatrol;

    void Update()
    {
        
    }
    /// <summary>
    /// Получить урон
    /// </summary>
    public void TakeDamage(GameObject player)
    {
        switch (typePatrol)
        {
            case TypePatrol.Air:
                gameObject.GetComponent<AirPatrol>().state = AirPatrol.State.Dead;
                break;
            case TypePatrol.Ground:
                gameObject.GetComponent<GroundPatrol>().state = GroundPatrol.State.Dead;
                break;
            case TypePatrol.NoPatrol:
                gameObject.GetComponent<Reaper>().KillMe(player);
                break;
        }
        Invoke("DestroyMe", 1f);
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
