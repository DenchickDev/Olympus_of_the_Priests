using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGuide : MonoBehaviour
{ 
    public Transform soulCount;
    public Transform lifeCount;
    public Transform reaper;
    public float speed;
    //�������� �� �������� 
    private bool soulStartToCount = false;
    public bool lifeStartToCount = false;
    public bool ReperRun = false;
    public Player player;
    //����� ������
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
    //����� �� �������� ������� 
    public void OnTriggerEnter2D(Collider2D collision)
    {   //������� ���� � ������� 
        if (collision.gameObject.tag == "Player")
        {
            soulStartToCount = true;
        }
        //������ ���� � �������
        if (collision.gameObject.tag == "SoulCount")
        {   //��������� � �������� +1 ���� � �������� ����
            //��������� ���� ��� ������� � �������
            player.SoulCount();
            destroySoul();
        }
        //������ ���� � �������
        if (collision.gameObject.tag == "LifeCount")
        {   //��������� � �������� +1 ���� � �������� ����
            //��������� ���� ��� ������� � �������
            player.GetComponent<Player>().LifeCount();
            destroySoul();
        }
    }
    //����� �������� ���� � �������� � �������� �� ������ 
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
