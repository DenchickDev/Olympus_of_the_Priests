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

    private void Start()
    {
        GameObject sword = GameObject.Find("Sword");
        GameObject arrowUp = GameObject.Find("ArrowUp");
        GameObject arrowDawn = GameObject.Find("ArrowDawn");
#if UNITY_STANDALONE || UNITY_EDITOR
        sword.SetActive(false);
        arrowUp.SetActive(false);
        arrowDawn.SetActive(false);
#else
        sword.SetActive(true);
        arrowUp.SetActive(true);
        arrowDawn.SetActive(true);
#endif
    }


    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Update()
    {
        soulText.text = player.GetCountUI().ToString();
        lifeText.text = player.life.ToString();
        //Проверка на нажатие клавишы ESC
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    //Метод вызова паузы
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
    //Метод активации паузы
    public void PauseOn()
    {
        CheckPause = true;
        player.enabled = false;
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
    //Метод дезактивации паузы
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


        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);
        print(PlayerPrefs.GetInt("Lvl")); 
    }
    


}
