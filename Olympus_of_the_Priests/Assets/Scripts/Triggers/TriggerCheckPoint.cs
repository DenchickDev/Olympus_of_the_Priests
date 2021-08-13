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
            collision.gameObject.GetComponent<Player>().OffControl();

            Invoke("EnableJump", 5);
            Invoke("EnableAll", 10);

        }
    }

    private void EnableJump()
    {
        print("Можешь прыгать");
        player.GetComponent<Player>().EnableJump();
    }

    private void EnableAll()
    {
        print("Можешь ВСЁ");
        player.GetComponent<Player>().OnControl();
    }
}
