using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    /// <summary>
    /// �������� ����
    /// </summary>
    public void TakeDamage()
    {
        Destroy(this.gameObject);
    }
}
