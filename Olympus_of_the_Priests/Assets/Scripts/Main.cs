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
    public GameObject PauseScreen;
    public GameObject WinScreen; 
    public GameObject PauseButton;
    private bool CheckPause = false;
   



    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Update()
    {
        soulText.text = player.GetCountUI().ToString();
        lifeText.text = player.life.ToString();
        //�������� �� ������� ������� ESC
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    //����� ������ �����
    private void Pause()
    {
        if (CheckPause == false)
        {
            PauseOn();
        }
        else
        {
            PauseOff();
        }
    }
    //����� ��������� �����
    public void PauseOn()
    {
        CheckPause = true;
        player.enabled = false;
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
    //����� ������������ �����
    public void PauseOff()
    {
        CheckPause = false;
        player.enabled = true;
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }
    public void Win()
    {
        player.enabled = true;
        Time.timeScale = 0f;
        WinScreen.SetActive(true);
        //Invoke("stopControl",4);
    }
    


}
