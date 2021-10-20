using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGuide : MonoBehaviour
{    /// <summary>
     /// игровой объект счетчик душ
     ///  координаты объекта счетчика душ 
     /// </summary>
    GameObject soulCountObject;
    Transform soulCountPosition;
    /// <summary>
    /// игровой объект счетчик здоровья
    ///  координаты объекта счетчика здоровья 
    /// </summary>
    GameObject lifeCountObject;
    Transform lifeCountPosition;
    public Transform reaper;
    public float speed;
    //Проверка на движение 
    private bool soulStartToCount = false;
    public bool lifeStartToCount = false;
    public bool ReperRun = false;
    GameObject PlayerObject;
    Player PlayerScript;
    /// <summary>
    /// точка лимита Х
    /// точка лимита У
    /// игровой объект точка лимита
    /// координаты объекта точка лимита
    /// </summary> 
    private float limit_x;
    private float limit_y;
    GameObject limitObject;
    Transform limitPosition;
    private void Awake()
    {
        soulCountObject = GameObject.Find("ImageSoul");
        soulCountPosition = soulCountObject.GetComponent<Transform>();
        lifeCountObject = GameObject.Find("ImageLife");
        lifeCountPosition = lifeCountObject.GetComponent<Transform>();
        limitObject = GameObject.Find("LimitSoulsAndLifeOrbs");
        limitPosition = limitObject.GetComponent<Transform>();
        PlayerObject = GameObject.Find("Player");
        PlayerScript = PlayerObject.GetComponent<Player>();

    }
    private void Start()
    {
       
    }

    private void Update()
    {
        limit_x = limitPosition.position.x;
        limit_y = limitPosition.position.y;
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
            PlayerScript.SoulCount();
            destroySoul();
        }
        //Ксание души о счетчик
        if (collision.gameObject.tag == "LifeCount")
        {   //добовляет к счетчику +1 душу и вызывает звук
            //разрушает душу при косании о счетчик
            PlayerScript.LifeCount();
            destroySoul();
        }
    }
    //метод движения душы к счетчику и движения за жнецом 
    private void FixedUpdate()
    {
        if (soulStartToCount == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, soulCountPosition.position, speed * Time.deltaTime);
        }
        if (lifeStartToCount == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, lifeCountPosition.position, speed * Time.deltaTime);
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
