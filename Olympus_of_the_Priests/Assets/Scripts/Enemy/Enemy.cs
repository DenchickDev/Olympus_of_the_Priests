using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ��� �������
    /// </summary>
    public enum TypePatrol
    {
        /// <summary>
        /// ������
        /// </summary>
        Ground,
        /// <summary>
        /// ���������
        /// </summary>
        Air,
        /// <summary>
        /// �� �����������
        /// </summary>
        NoPatrol
    }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public TypePatrol typePatrol = TypePatrol.NoPatrol;

    void Update()
    {
        
    }
    /// <summary>
    /// �������� ����
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
