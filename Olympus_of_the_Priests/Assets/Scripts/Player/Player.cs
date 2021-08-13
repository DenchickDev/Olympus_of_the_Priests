using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Main main;
    int soulsCount = 0;
    bool onRollover = false;
    ///<summary>
    /// �������� ��������
    /// �������� �����
    /// ������ ����� ����
    /// </summary>
    [SerializeField]
    public SoundManager soundManager;
    public AudioSource audioSource;
    public AudioClip[] soundsRun;

    /// <summary>
    /// ����� ����� 
    /// ����� ���� �������  
    /// </summary>
    public bool isMovement = false;
    public bool isSoundRunPlay = false;

    /// <summary>
    /// ����� ���������� � ���������
    /// </summary>
    [SerializeField]
    public float timeRollover = 1;

    /// <summary>
    /// �������� ��������� �������� �������� � �������� 
    /// </summary>
    [SerializeField]
    public float speedRollover = 1;

    /// <summary>
    /// ����������������� ������ ������� � ��
    /// </summary>
    private float timeOfOneBlink = 15f;

    /// <summary>
    /// ���-�� �������
    /// </summary>
    [SerializeField]
    private int CountBlinks = 4;

    /// <summary>
    /// ����������������� ������� � ���
    /// </summary>
    [SerializeField]
    private float timeBlinking = 2f;

    /// <summary>
    /// ����������������� ��� ���� � ���
    /// </summary>
    [SerializeField]
    private float timeGodMode = 2f;

    /// /// <summary>
    /// ����� ����������
    /// </summary>
    [SerializeField]
    private bool isGodMod = false;

    /// <summary>
    /// �������� ������������
    /// </summary>
    [SerializeField]
    float speed = 2.3f;

    /// <summary>
    /// ����� ��������: 
    /// true - ����� ���������,
    /// false - ����� � ������� ������
    /// </summary> 
    public bool controlMode = true;

    /// <summary>
    /// ���-�� ������ �� ������ ������
    /// </summary>
    [SerializeField]
    public int life;

    /// <summary>
    ///  ������������ ���-�� ������ 
    /// </summary>
    private int MaxLife = 100;

    private new Collider2D collider;

    /// <summary>
    /// ������� ���������������
    /// ����� � ������
    /// </summary>
    private bool isGrounded = true;

    /// <summary>
    /// ������������ �����
    /// </summary>
    [SerializeField]
    Transform groundCheck;
    public float CheckRadius;


    public enum State
    {
        /// <summary>
        /// ��������� �����
        /// </summary>
        Idle,
        /// <summary>
        /// ���
        /// </summary>
        Running,
        /// <summary>
        /// ������
        /// </summary>
        Jumping,
        /// <summary>
        /// �������
        /// </summary>
        Falling,
        /// <summary>
        /// ������
        /// </summary>
        Dead,
        /// <summary>
        /// �������
        /// </summary>
        Hurt,
        /// <summary>
        /// ��������
        /// </summary>
        Combustion,
        /// <summary>
        /// ����
        /// </summary>
        Stab,
        /// <summary>
        /// �������
        /// </summary>
        Rollover,
        /// <summary>
        /// ������������ 
        /// </summary>
        Sawing,
        /// <summary>
        /// ������������ 
        /// </summary>
        Crushed,
        /// <summary>
        /// ������������ 
        /// </summary>
        SawingInRollover,
        /// <summary>
        /// ������� � ��� � ������
        /// </summary>
        PitWithSpikes
    }

    /// <summary>
    /// ������� ��������� ���������
    /// </summary>
    public State state = State.Idle;

    /// <summary>
    /// ������� �����
    /// </summary>
    public Transform attackPos;

    /// <summary>
    /// ������ �������� �����
    /// </summary>
    public float attackRange;

    public ActionButtonsPlayer actionButtons;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        //life = MaxLife;
        timeOfOneBlink = timeBlinking / CountBlinks;
        //Application.targetFrameRate = 7;

        actionButtons = new ActionButtonsPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.S) && isGrounded && onRollover == false && state !=State.Dead && state != State.Rollover && state != State.Crushed && state != State.SawingInRollover && state != State.PitWithSpikes)
        if (actionButtons.CheckJump() &&
            !actionButtons.CheckRollover() &&
            isGrounded &&
            onRollover == false &&
            state != State.Dead &&
            state != State.Rollover &&
            state != State.Crushed &&
            state != State.SawingInRollover &&
            state != State.PitWithSpikes)
        {
            Jump();
        }
        else if (state != State.Dead && state != State.Combustion && state !=State.Sawing && actionButtons.CheckAttack() && onRollover == false && state != State.PitWithSpikes && state !=State.Stab && state !=State.Crushed)
        {
            state = State.Stab;
            soundManager.PlayHitSound();
        }
        if (controlMode)
        {
            if (actionButtons.isEnableMoveForever())
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            Movement();
        }

        CalculateState();
        anim.SetInteger("stateAnim", (int)state);
        //Debug.Log("ddddd");
        if (state == State.Combustion || state == State.Jumping)
            print(state.ToString());
        //print(GetComponent<SpriteRenderer>().color.g);
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x + 100, transform.position.y, transform.position.z));
        
    }
    //����� ����������� ����� �� ����� ������� ������������ ������ Update
    private void LateUpdate()
    {
        if (actionButtons.CheckRollover() && isGrounded && state != State.Dead && state !=State.PitWithSpikes && !Input.GetKeyDown(KeyCode.Space) &&  state != State.Crushed && state != State.SawingInRollover)
        {
            OnRollover();
        }
    }
    public void OnControl()
    {
        actionButtons.SetEnableAllLite(true);
    }
    public void OffControl()
    {
        actionButtons.SetEnableAllLite(false);
    }

    public void EnableJump()
    {
        actionButtons.SetEnableJump(true);
    }

    public void DisableJump()
    {
        actionButtons.SetEnableJump(false);
    }

    public void OffControlHard()
    {
        actionButtons.enableAllHard = false;
    }

    public void OnControlHard()
    {
        actionButtons.enableAllHard = true;
        actionButtons.SetEnableAllLite(true);
    }
    public void OnAttak()
    {
        //������ �����
        //���������� ��� ���������, ������� ����������
        //� ������ attackRange ������������ ������� attackPos
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        for (int i = 0; i < enemies.Length; i++)
        {
            var nameMask = LayerMask.LayerToName(enemies[i].gameObject.layer);
            if (nameMask.Contains("Enemy"))
            {
                var killableEnemy = enemies[i].GetComponent<IKillable>();
                if (killableEnemy != null)
                {
                    killableEnemy.KillMe();
                }
            }
        }
    }

    public void SetDefaultState()
    {
        state = State.Idle;
    }

    /// <summary>
    /// ������������ ������ ����� �� �����,
    /// ��� ��������� �������
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    void FixedUpdate()
    {
        //if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        //���� ��������� �� ���������� �� ����� "Ground", �� �� �� �����
        //isGrounded = collider.IsTouchingLayers(ground); // ���������
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
    }

    /// <summary>
    /// ����� ��������� ������� ������� �������� � �������
    /// </summary>
    void Movement()
    {
        if (actionButtons.CheckMove())
        {
            rb.velocity = new Vector2(actionButtons.GetMove() * speed, rb.velocity.y);
            isMovement = true;
            PlayRunSound();

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMovement = false;
        }

    }

    //����� ������������ ������
    void Jump()
    {
        rb.AddForce(transform.up * 8f, ForceMode2D.Impulse);
        soundManager.PlayJumpSound();
    }

    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    void CalculateState()
    {
        //if (state == State.Jumping)
        //{
        //}
        if ( state != State.SawingInRollover && state != State.PitWithSpikes && state != State.Dead && state != State.Combustion  && state != State.Sawing && state != State.Crushed && state != State.Stab && state != State.Rollover)
        {
            if ( rb.velocity.y < -.1f && state != State.Crushed && !isGrounded )
            {
                state = State.Falling;
            }
            else if (rb.velocity.y >.1f  && state != State.Crushed)
            {
                state = State.Jumping;
            }
            else if (state == State.Falling)
            {
                if (rb.velocity.y >= -.1f)
                {
                    state = State.Idle;
                }
            }
            else if (Mathf.Abs(rb.velocity.x) > 2f)
            {
                state = State.Running;
            }
            else 
            {
                state = State.Idle;
            }
        }
    }

    public void SetDamageWithGodMode(int deltaLife)
    {
        if (!isGodMod)
        {
            StartBlinking();
            this.RecountLife(-deltaLife);
            isGodMod = true;
            Invoke("TurnOffGodeMode", timeGodMode);
        }
    }

    /// <summary>
    /// ������� ����� ����
    /// </summary>
    /// <param name="damage">���������� �����</param>
    public void SetDamage(int damage)
    {
        this.RecountLife(-damage);
    }

    private void TurnOffGodeMode()
    {
        isGodMod = false;
    }

    //����� ������������ ���-�� �����, �� ��������� ��������� ����� ��������� �������� � ���������� ������ 
    public void RecountLife(int deltaLife)
    {
        if (deltaLife > 0 && (life + deltaLife) > MaxLife)
        {
            life = MaxLife;
        }
        else if ((life + deltaLife) < 0)
        {
            life = 0;
        }
        else
        {
            life = life + deltaLife;
        }
        //print(life);
        if (life <= 0)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            Invoke("Lose", 2f);
            state = State.Dead;

        }
    }

    //����� ������ ������������ ������ ����� ��� 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            KillPerson();
            state = State.Combustion;
        }
        if(collision.gameObject.tag == "Saw")
        {
            KillPerson();
            if (state == State.Rollover)
            {
                state = State.SawingInRollover;
            }
            else
            {
                state = State.Sawing;
            }   
        }
        if (collision.gameObject.tag == "Rock" && isGrounded)
        {
            KillPerson();
             state = State.Crushed;
        }
        if (collision.gameObject.tag == "PitWithSpikes")
        {
            KillPerson();
            state = State.PitWithSpikes;
        }

    }
    //����� ������������ ������
    public void KillPerson()
    {
        SetDamage(life);
    }

    /// <summary>
    /// ��������� ������� (�������� ������������ �������)
    /// </summary>
    void StartBlinking()
    {
        StopCoroutine("Blinking");
        StartCoroutine("Blinking");
    }

    /// <summary>
    /// �������� � ��������
    /// </summary>
    /// <returns></returns>
    IEnumerator Blinking()
    {
        float time = timeOfOneBlink / 2.0f; //����� �� ����� ����� �������� (�� ����� 1 �� ������ 0 � ��������)
        float rate = 1.0f / time;
        Color defaultColor = GetComponent<SpriteRenderer>().color;
        defaultColor.a = 1;
        Color next = defaultColor;
        Color current = defaultColor;
        current.a = 0;

        //���� ������� ������� �� ���� ������:
        //������ ����� �����, ����� �������� ���, ������� *2
        int countAllBlinks = CountBlinks * 2;

        for (int curBlik = 0; curBlik < countAllBlinks; curBlik++)
        {
            SwapColors(ref current, ref next);
            float i = 0.0f;
            while (i < 1.0f)
            {
                i += Time.fixedDeltaTime * rate;
                GetComponent<SpriteRenderer>().color = Color.Lerp(current, next, i);
                yield return new WaitForFixedUpdate();
            }
        }
        //������� ���������, ��� ��� ����� ����� ����� ����� 1
        GetComponent<SpriteRenderer>().color = defaultColor;
        yield break;
    }

    /// <summary>
    /// �������� ������� color1 � color2
    /// </summary>
    /// <param name="color1">������ ����</param>
    /// <param name="color2">������ ����</param>
    void SwapColors(ref Color color1, ref Color color2)
    {
        Color temp;
        temp = color1;
        color1 = color2;
        color2 = temp;
    }

    //����� ����������� ����� 
    public void Lose()
    {
        main.gameObject.GetComponent<Main>().Lose();
    }
    //����� ����� ���� � ������ 
    public void SoulCount()
    {
        soulsCount++;
        soundManager.PlayTakeItemsSound();
    }
    //����� ��������� � ������ 
    public void LifeCount()
    {
        RecountLife(10);
        soundManager.PlayHillSound();
    }
    //����� ������ ����� ���� 
    private void PlayRunSound()
    {

        if (isGrounded && onRollover == false && state != State.Rollover && state != State.Jumping )
        {
            if (isMovement == true && isSoundRunPlay == false)
            {
                soundManager.PlayRunSound();
                isSoundRunPlay = true;
                isMovement = false;

            }
            else
            {
               
            }
        }
    }    
   
    //����� �������� ������ � ������� 
    public int GetCountUI()
    {
        return soulsCount;
        //return life;
    }
    //����� ��������� �������
    private void OnRollover()
    {
      if(onRollover == false)
      {
        speed += speedRollover;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = true;
        state = State.Rollover;
        Invoke("OffRollover", timeRollover);
        onRollover = true;
        soundManager.PlayWoundSound();
      }
    }
    //����� ���������� �������
    private void OffRollover()
    {
        speed -= speedRollover;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        //state = State.Idle;
        onRollover = false;
        if (isGrounded && state != State.Crushed && state != State.Dead && state != State.SawingInRollover && state != State.Stab)
        {
            state = State.Idle;
        }
        if(!isGrounded && state !=State.Combustion)
        {
            state = State.Falling;
        }
    }
    //����� ������ ����� ���� ��� ����������� ������� �� ��������
    private void soundsRunControl()
    {
        int randomInt = Random.Range(0, soundsRun.Length);
        audioSource.PlayOneShot(soundsRun[randomInt]);
    }
}




