using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigidbody2D;
    //private bool isDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSimulated(bool _simulate)
    {
        rigidbody2D.simulated = _simulate;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            anim.SetBool("isDestroyed", true);
            Invoke("DestroyMe", 0.8f);

        }
    }
    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
