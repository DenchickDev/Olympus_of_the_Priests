using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{

    Animator anim;
    Rigidbody2D rb;

    /// <summary>
    /// Скорость передвижения
    /// </summary>
    [SerializeField]
    float speed = 2.3f;
    public SoundManager soundManager;

    /// <summary>
    /// Время до уничтожения жнеца
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
        Invoke("DestroyMe", timeToDestroy);
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void KillMe(GameObject player)
    {
        rb.simulated = false;
        isRuning = false;
        gameObject.GetComponent<SoulGuide>().DeadReaper();
        //player.GetComponent<Player>().RecountLife(10);
        //soundManager.PlayHillSound();
        anim.SetBool("isDead", true);
    }
}
