using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
      public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    //Метод продолжения игры при смене уровня 
   public void startingTime (bool startTime)
    {
        if (startTime == true)
        {
            Time.timeScale = 1f;
        }

    }
}
