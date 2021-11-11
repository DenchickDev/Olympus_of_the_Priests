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
    private static bool CheckMusicMute = false;
    private bool ChoicePanelOn = false;
    public Button [] lvls;
    public Button ResumeButton;
    private int resume;
    public bool saveMode;
    private GameObject BackgraundMusic;
    


    private void Start()
    {
        BackgoungMusic = GameObject.Find("BackgraundMusic");
        if (CheckMusicMute == false)
        {
            ButtonMusicOff.SetActive(true);
            ButtonMusicOn.SetActive(false);
          
        }
        else
        {
            ButtonMusicOff.SetActive(false);
            ButtonMusicOn.SetActive(true);
        }
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
   
    public void Resume()
    {
        SceneManager.LoadScene(resume);
    }
    public void DellKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    public void OpenNextLvlScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Time.timeScale = 1f;
        /*if (SceneManager.GetActiveScene().name == ("Disclamer") & SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            Destroy(BackgoungMusic);
        }*/
    }
    public void OpenSceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(BackgoungMusic);
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
            
    }
    public  void ButtonMusicMute ()
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
