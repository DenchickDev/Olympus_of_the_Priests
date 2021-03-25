using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text soulText;
    public Text lifeText;
    //public GameObject PauseScreen;
    public GameObject WinScreen;

    

   public void Lose ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Update()
    {
        soulText.text = player.GetCountUI().ToString();
        lifeText.text = player.life.ToString();

    }
    public void Win()
    {
        player.enabled = true;
        Time.timeScale = 0f;
        WinScreen.SetActive(true);
        //Invoke("stopControl",4);
    }
    void stopControl()
    {
     //player.enabled = true;
    }
}
