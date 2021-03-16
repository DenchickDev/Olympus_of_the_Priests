using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            collision.gameObject.GetComponent<Player>().SetDamageWithGodMode(damage);
            
        }
    }
}
