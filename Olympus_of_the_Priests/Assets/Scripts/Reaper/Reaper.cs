using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour, IKillable
{

    Animator anim;
    Rigidbody2D rb;

    /// <summary>
    /// Скорость передвижения
    /// </summary>
    [SerializeField]
    float speed = 2.3f;
    SoundManager soundManager;
    GameObject MainCamera;
    public SoulGuide soulGuide;
    public bool soulToDestroy  = false;
    public bool SoulMuve = false;

    /// <summary>
    /// Время до уничтожения жнеца
    /// </summary>
    [SerializeField]
    float timeToDestroy = 7f;

    public bool isRuning = false;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        soundManager = MainCamera.GetComponent<SoundManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRuning)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    public void Run()
    {
        isRuning = true;
        soulGuide.GetComponent<SoulGuide>().ReperRun = true;
        Invoke("DestroyObject", timeToDestroy);
    }

    private void DestroyObject()
    {
        if (isRuning == true)
        { 
            soulGuide.GetComponent<SoulGuide>().destroySoul();
        } 
            Destroy(this.gameObject); 
        
    }

    public void KillMe()
    {
        rb.simulated = false;
        isRuning = false;
        anim.SetBool("KillPerson", true);
        soulGuide.GetComponent<SoulGuide>().DeadReaper();
        //player.GetComponent<Player>().RecountLife(10);
        //soundManager.PlayHillSound();
        
    }
}
