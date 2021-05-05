using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject ChoiceLevelButtonOn;
    public GameObject ChoiceLevelPanel;
    public GameObject ButtonMusicOff;
    public GameObject ButtonMusicOn;
    public GameObject BackgoungMusic;
    private bool CheckMusicMute = false;
    private bool ChoicePanelOn = false;
    public Button [] lvls;
    public Button ResumeButton;
    private int resume;
    public bool saveMode;
    
    private void Start()
    {
        if (saveMode == true)
        {
            if (PlayerPrefs.HasKey("Lvl"))
                for (int i = 0; i < lvls.Length; i++)
                {
                    if (i <= PlayerPrefs.GetInt("Lvl"))
                    {
                        lvls[i].interactable = true;
                        resume = PlayerPrefs.GetInt("Lvl") + 1;
                        ResumeButton.interactable = true;
                    }

                    else
                        lvls[i].interactable = false;
                }
        }
    }
    public void DontSavingBGMusic()
    {
        gameObject.GetComponent<SoundManager>().DestroyBGMusic();
    }
    public void Resume()
    {
        SceneManager.LoadScene(resume);
    }
    public void DellKeys()
    {
        PlayerPrefs.DeleteAll();
    }
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
    private void FixedUpdate()
    {
        if (AudioListener.volume == 0 && CheckMusicMute == false )
        {
            ButtonMusicOff.SetActive(false);
            ButtonMusicOn.SetActive(true);
            CheckMusicMute = true;
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
            //BackgoungMusic.gameObject.GetComponent<AudioSource>().mute = true;
            AudioListener.volume = 0;
        }
        else
        {
            //BackgoungMusic.gameObject.GetComponent<AudioSource>().mute = false;
            AudioListener.volume = 1;
        }
    }
}
