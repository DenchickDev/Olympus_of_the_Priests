using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPoint : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.OffControl();

            SaveData data = new SaveData(player);
            data.positionPlayer[0] -= 3f;
            SaveLoad.SaveGame(data); //��������� ����

            //print("��������");
            Invoke("EnableJump", 1);
            Invoke("EnableAll", 1);

        }
    }

    private void EnableJump()
    {
        //print("������ �������");
        player.GetComponent<Player>().EnableJump();
    }

    private void EnableAll()
    {
        //print("������ �Ѩ");
        player.GetComponent<Player>().OnControl();
    }
}
