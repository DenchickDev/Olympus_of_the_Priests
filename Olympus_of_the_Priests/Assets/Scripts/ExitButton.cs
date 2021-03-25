using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void exitButton()
    {
        Application.Quit();
    }
}
