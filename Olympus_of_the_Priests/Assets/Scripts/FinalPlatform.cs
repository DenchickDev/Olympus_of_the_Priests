using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalPlatform : MonoBehaviour
{
  
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().playOnAwake = true;

                /*  if (collision.gameObject.tag == "Player")
          {
              GetComponent<PlayableDirector>().playOnAwake = true;
             // GetComponent<Player>().state = Player.State.Running;
          }*/
            }
        } 
    
}
