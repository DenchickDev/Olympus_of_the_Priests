using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour, IKillable
{

    Animator anim;
    Rigidbody2D rb;

    /// <summary>
    /// �������� ������������
    /// </summary>
    [SerializeField]
    float speed = 2.3f;
    public SoundManager soundManager;
    public SoulGuide soulGuide;
    public bool soulToDestroy  = false;
    public bool SoulMuve = false;

    /// <summary>
    /// ����� �� ����������� �����
    /// </summary>
    [SerializeField]
    float timeToDestroy = 7f;

    public bool isRuning = false;
    // Start is called before the first frame update
    void Start()
    {
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
        Invoke("DestroyMe", timeToDestroy);
    }

    private void DestroyMe()
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
        Invoke("DestroyMe", 1f);
    }
}
