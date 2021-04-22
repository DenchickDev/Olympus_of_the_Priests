using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    /// <summary>
    /// Время после прикосновения и до падения
    /// </summary>
    [SerializeField]
    private float timeToFall = 0.5f;

    /// <summary>
    /// Время после прикосновения и до уничтожения
    /// </summary>
    [SerializeField]
    private float timeToDestroy = 2f; 
    //Переменные для звукового сопровождения
    public AudioClip fallingSound;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Player")
      {
            Invoke("FallingPlatform", timeToFall);
            Destroy(gameObject, timeToDestroy);
            audioSource.PlayOneShot(fallingSound);
        }
      
    }

    private void FallingPlatform ()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
