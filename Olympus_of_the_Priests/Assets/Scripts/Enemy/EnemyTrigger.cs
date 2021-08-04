using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField]
    public int damage = 10;
    bool noDamage = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!noDamage)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().SetDamageWithGodMode(damage);

            }
        }
    }
    public void DestroyOjectDamage()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Перестать наносить урон
    /// </summary>
    public void MakeNoDamaging()
    {
        noDamage = true;
    }
}
