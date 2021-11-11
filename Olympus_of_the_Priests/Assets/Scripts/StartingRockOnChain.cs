using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRockOnChain : MonoBehaviour
{
    [SerializeField]
    Transform rock;
    //Переменные для звукового сопровождения
    public AudioClip fallingSound;
    private GameObject audioSourceGameObject;
    private AudioSource audioSource;
    
    void Start ()
    {
        audioSourceGameObject = GameObject.Find("SoundManger");
        audioSource = audioSourceGameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rock.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            audioSource.PlayOneShot(fallingSound);
            
        }
    }
}
