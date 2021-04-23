using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGuide : MonoBehaviour
{ 
        public Transform soulCount;
        public Transform lifeCount;
        public float speed;
        public SoundManager soundManager;
        //�������� �� �������� 
        private bool soulStartToTarrget = false;
        private bool lifeStartToTarrget = false;
        public Player player;

        public void OnTriggerEnter2D(Collider2D collision)
        {//������� ���� � ������� 
            if (collision.gameObject.tag == "Player")
            {
                soulStartToTarrget = true;
            }//������ ���� � �������
            if (collision.gameObject.tag == "SoulCount")
            {   //��������� � �������� +1 ���� � �������� ����
                //��������� ���� ��� ������� � �������
                player.SoulCount();
                Destroy(gameObject);
            }
            //������ ���� � �������
            if (collision.gameObject.tag == "lifeCount")
            {   //��������� � �������� +1 ���� � �������� ����
                //��������� ���� ��� ������� � �������
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
