using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartNextScene", 5f);
    }
    private void StartNextScene()
    {
        SceneManager.LoadScene(1);
    }
   
}
