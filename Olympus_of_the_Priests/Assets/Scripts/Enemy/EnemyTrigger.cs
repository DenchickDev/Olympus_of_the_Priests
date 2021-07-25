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
                noDamage = true;

                collision.gameObject.GetComponent<Player>().SetDamageWithGodMode(damage);
                Invoke("SetDamage",2f);

            }
        }
    }
    public void DestroyOjectDamage()
    {
        Destroy(this.gameObject);
    }
    void SetDamage()
    {
        noDamage = false;
    }
}
