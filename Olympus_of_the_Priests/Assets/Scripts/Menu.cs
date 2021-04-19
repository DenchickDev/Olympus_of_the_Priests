using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject ChoiceLevelButtonOn;
    public GameObject ChoiceLevelPanel;
    public GameObject ButtonMusicOff;
    public GameObject ButtonMusicOn;
    public GameObject BackgoungMusic;
    private bool CheckMusicMute = false;
    private bool ChoicePanelOn = false;

      public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }
    //Метод продолжения игры при смене уровня 
   public void startingTime (bool startTime)
    {
        if (startTime == true)
        {
            Time.timeScale = 1f;
        }

    }
    //Метод вызова меню выбора уровней
   public void choiceLevelButtonOn ()
    {
        if (ChoicePanelOn == false)
        {
            ChoiceLevelPanel.SetActive(true);
            ChoicePanelOn = true;
        }
        else
        {
            ChoiceLevelPanel.SetActive(false);
            ChoicePanelOn = false;
        } 
            
    }public void ButtonMusicMute ()
    {
        if (CheckMusicMute == false)
        {
            ButtonMusicOff.SetActive(false);
            ButtonMusicOn.SetActive(true);
            MusicMute();
            CheckMusicMute = true;
        }
        else
        {
            ButtonMusicOff.SetActive(true);
            ButtonMusicOn.SetActive(false);
            MusicMute();
            CheckMusicMute = false;
        } 
            
    }
    
    private void MusicMute ()
    {
        if (CheckMusicMute == false)
        {
            BackgoungMusic.gameObject.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            BackgoungMusic.gameObject.GetComponent<AudioSource>().mute = false;
        }
    }
}
