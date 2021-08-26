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
    public bool lifeStartToCount = false;
    public bool ReperRun = false;
    public Player player;
    //Точки лимита
    public Transform limit;
    private float limit_x;
    private float limit_y;
    private Transform limitTransform;
    private void Start()
    {
        limitTransform = limit.GetComponent<Transform>();
    }

    private void Update()
    {
        limit_x = limitTransform.position.x;
        limit_y = limitTransform.position.y;
        if (soulStartToCount == true)
            if (this.gameObject.transform.position.x < limit_x)
            {
                this.gameObject.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);
            }
        if (this.gameObject.transform.position.y > limit_y)
        {
            this.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
        }
    }
    //Метод на проверку косания 
    public void OnTriggerEnter2D(Collider2D collision)
    {   //Косание души с игроком 
        if (collision.gameObject.tag == "Player")
        {
            soulStartToCount = true;
        }
        //Ксание души о счетчик
        if (collision.gameObject.tag == "SoulCount")
        {   //добовляет к счетчику +1 душу и вызывает звук
            //разрушает душу при косании о счетчик
            player.SoulCount();
            destroySoul();
        }
        //Ксание души о счетчик
        if (collision.gameObject.tag == "LifeCount")
        {   //добовляет к счетчику +1 душу и вызывает звук
            //разрушает душу при косании о счетчик
            player.GetComponent<Player>().LifeCount();
            destroySoul();
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
   public void destroySoul()
    {
        Destroy(gameObject);
    }
}
