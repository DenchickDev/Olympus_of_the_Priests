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
        private bool lifeStartToCount = false;
        public bool ReperRun = false;
        public Player player;
       

    public void OnTriggerEnter2D(Collider2D collision)
    {   //������� ���� � ������� 
        if (collision.gameObject.tag == "Player")
        {
            soulStartToCount = true;
        }//������ ���� � �������
        if (collision.gameObject.tag == "SoulCount")
        {   //��������� � �������� +1 ���� � �������� ����
            //��������� ���� ��� ������� � �������
            player.SoulCount();
            Destroy(gameObject);
        }
        //������ ���� � �������
        if (collision.gameObject.tag == "LifeCount")
        {   //��������� � �������� +1 ���� � �������� ����
            //��������� ���� ��� ������� � �������
            player.GetComponent<Player>().LifeCount();
            Destroy(gameObject);
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
    
    
}
