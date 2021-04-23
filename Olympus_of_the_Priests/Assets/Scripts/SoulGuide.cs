using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGuide : MonoBehaviour
{ 
        public Transform soulCount;
        public Transform lifeCount;
        public Transform reaper;
        public float speed;
        //Проверка на движение 
        private bool soulStartToCount = false;
        private bool lifeStartToCount = false;
        public bool ReperRun = false;
        public Player player;
       

    public void OnTriggerEnter2D(Collider2D collision)
    {   //Косание души с игроком 
        if (collision.gameObject.tag == "Player")
        {
            soulStartToCount = true;
        }//Ксание души о счетчик
        if (collision.gameObject.tag == "SoulCount")
        {   //добовляет к счетчику +1 душу и вызывает звук
            //разрушает душу при косании о счетчик
            player.SoulCount();
            Destroy(gameObject);
        }
        //Ксание души о счетчик
        if (collision.gameObject.tag == "LifeCount")
        {   //добовляет к счетчику +1 душу и вызывает звук
            //разрушает душу при косании о счетчик
            player.GetComponent<Player>().LifeCount();
            Destroy(gameObject);
        }
    }
    //метод движения душы к счетчику и движения за жнецом 
    private void FixedUpdate()
     {
      if (soulStartToCount == true)
      {
        transform.position = Vector3.MoveTowards(transform.position, soulCount.position, speed * Time.deltaTime);
      }
        if (lifeStartToCount == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, lifeCount.position, speed * Time.deltaTime);
        }
       if (ReperRun == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, reaper.position, speed * Time.deltaTime);
        }

    }
        public void DeadReaper()
        {
            lifeStartToCount = true;
            ReperRun = false;
        }
    
    
}
