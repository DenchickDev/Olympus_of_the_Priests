using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Player")
      {
            Invoke("FallingPlatform", 0.5f);
            Destroy(gameObject, 2f); 
      }
      
    }
    private void FallingPlatform ()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
