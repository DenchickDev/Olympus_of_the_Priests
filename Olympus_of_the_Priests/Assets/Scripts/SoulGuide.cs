using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGuide : MonoBehaviour
{ 
        public Transform soulCount;
        public Transform lifeCount;
        public float speed;
        public SoundManager soundManager;
        //Проверка на движение 
        private bool soulStartToTarrget = false;
        private bool lifeStartToTarrget = false;
        public Player player;

        public void OnTriggerEnter2D(Collider2D collision)
        {//Косание души с игроком 
            if (collision.gameObject.tag == "Player")
            {
                soulStartToTarrget = true;
            }//Ксание души о счетчик
            if (collision.gameObject.tag == "SoulCount")
            {   //добовляет к счетчику +1 душу и вызывает звук
                //разрушает душу при косании о счетчик
                player.SoulCount();
                Destroy(gameObject);
            }
            //Ксание души о счетчик
            if (collision.gameObject.tag == "lifeCount")
            {   //добовляет к счетчику +1 душу и вызывает звук
                //разрушает душу при косании о счетчик
                player.RecountLife(10);
                soundManager.PlayHillSound();
                Destroy(gameObject);
            }

        }

        private void FixedUpdate()
        {
            if (soulStartToTarrget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, soulCount.position, speed * Time.deltaTime);
            }
            if (lifeStartToTarrget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, lifeCount.position, speed * Time.deltaTime);
            }
        }
        public void DeadReaper()
        {
            lifeStartToTarrget = true;
        }
    
}
