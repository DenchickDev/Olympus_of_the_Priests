using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorSceeneManager : MonoBehaviour
{
    public GameObject TutorScreen;
    public GameObject ButtonOk;

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
    }
    public void onTutorScreen()
    {
        TutorScreen.SetActive(true);
    }
}
