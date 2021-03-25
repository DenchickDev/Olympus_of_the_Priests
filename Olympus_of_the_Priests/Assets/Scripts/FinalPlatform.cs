using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalPlatform : MonoBehaviour
{
    public Main main;
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
             main.Win();
            }
        
    } 

    
}
