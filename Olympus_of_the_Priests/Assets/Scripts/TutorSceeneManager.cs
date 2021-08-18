using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorSceeneManager : MonoBehaviour
{
    public GameObject TutorScreen;
    public GameObject ButtonOk;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onTutorScreen();
        }
    }
    public void offTutorScreen()
    {
        TutorScreen.SetActive(false);
        player.GetComponent<Player>().OnControl();
    }
    public void onTutorScreen()
    {
        TutorScreen.SetActive(true);
    }
}
