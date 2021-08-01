using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    bool isHit = false;
    public Main main;
    int soulsCount = 0;
    bool onRollover = false;
    ///<summary>
    /// Звуковой менеджер
    /// Источник звука
    /// Массив звука бега
    /// </summary>
    [SerializeField]
    public SoundManager soundManager;
    public AudioSource audioSource;
    public AudioClip[] soundsRun;

    /// <summary>
    /// Игрок бежит 
    /// саунд бега запущен  
    /// </summary>
    public bool isMovement = false;
    public bool isSoundRunPlay = false;

    /// /// <summary>
    /// Режим бессмертия
    /// </summary>
    [SerializeField]
    private bool isGodMod = false;

    /// <summary>
    /// Время одного мигания в мс
    /// </summary>
    [SerializeField]
    private float timeOfOneBlink = 15f;

    /// <summary>
    /// Время прибывания в перекатеи
    /// </summary>
    [SerializeField]
    public float timeRollover = 1;
    /// <summary>
    /// Значение повышения скорости движения в перекате 
    /// </summary>
    [SerializeField]
    public float speedRollover = 1;
    /// <summary>
    /// Кол-во миганий
    /// </summary>
    [SerializeField]
    private int CountBlinks = 3;

    /// <summary>
    /// Кол-во миганий
    /// </summary>
    [SerializeField]
    private int CurrentCountBlinks = 0;

    /// <summary>
    /// Скорость передвижения
    /// </summary>
    [SerializeField]
    float speed = 2.3f;

    /// <summary>
    /// Режим движения: 
    /// true - бежит автоматом,
    /// false - бежит с помощью клавиш
    /// </summary> 
    public bool controlMode = true;

    /// <summary>
    /// Кол-во жизней на данный момент
    /// </summary>
    [SerializeField]
    public int life;
    /// <summary>
    ///  Максимальное кол-во жизней 
    /// </summary>
    private int MaxLife = 100;

    private new Collider2D collider;

    /// <summary>
    /// Признак соприкосновения
    /// героя с землей
    /// </summary>
    private bool isGrounded = true;

    /// <summary>
    /// Расположение земли
    /// </summary>
    [SerializeField]
    Transform groundCheck;
    public float CheckRadius;


    public enum State
    {
        /// <summary>
        /// Состояние покоя
        /// </summary>
        Idle,
        /// <summary>
        /// Бег
        /// </summary>
        Running,
        /// <summary>
        /// Прыжок
        /// </summary>
        Jumping,
        /// <summary>
        /// Падение
        /// </summary>
        Falling,
        /// <summary>
        /// Смерть
        /// </summary>
        Dead,
        /// <summary>
        /// Ранение
        /// </summary>
        Hurt,
        /// <summary>
        /// Сгорание
        /// </summary>
        Combustion,
        /// <summary>
        /// Удар
        /// </summary>
        Stab,
        /// <summary>
        /// перекат
        /// </summary>
        Rollover,
        /// <summary>
        /// распиливание 
        /// </summary>
        Sawing,
        /// <summary>
        /// распиливание 
        /// </summary>
        Crushed,
        /// <summary>
        /// распиливание 
        /// </summary>
        SawingInRollover,
        /// <summary>
        /// Падение в яму с шипами
        /// </summary>
        PitWithSpikes
    }

    /// <summary>
    /// Текущее состояние персонажа
    /// </summary>
    public State state = State.Idle;

    /// <summary>
    /// Позиция атаки
    /// </summary>
    public Transform attackPos;

    /// <summary>
    /// Радиус действия атаки
    /// </summary>
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        life = MaxLife;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.S) && isGrounded && onRollover == false && state != State.Rollover && state != State.Crushed && state != State.SawingInRollover && state != State.PitWithSpikes)
        {
            Jump();
        }
        else if (state != State.Dead && state != State.Combustion && state !=State.Sawing && Input.GetMouseButtonDown(0) && onRollover == false && state != State.PitWithSpikes && state !=State.Stab && state !=State.Crushed)
        {
            state = State.Stab;
            soundManager.PlayHitSound();
        }
        if (controlMode)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
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
    //Метод срабатывает сразу же после полного срабатывания метода Update
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && state !=State.PitWithSpikes && !Input.GetKeyDown(KeyCode.Space) &&  state != State.Crushed && state != State.SawingInRollover)
        {
            OnRollover();
        }
    }
    public void OnAttak()
    {
        //собсна атака
        //возвращаем все колайдеры, которые попадуться
        //в радиус attackRange относительно позиции attackPos
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        for (int i = 0; i < enemies.Length; i++)
        {
            var nameMask = LayerMask.LayerToName(enemies[i].gameObject.layer);
            if (nameMask.Contains("Enemy"))
            {
                //enemies[i].GetComponent<Enemy>().TakeDamage(gameObject);
                if (enemies[i].GetComponent<AirPatrol>())
                {
                    enemies[i].GetComponent<AirPatrol>().state = AirPatrol.State.Dead;
                }
                else if(enemies[i].GetComponent<GroundPatrol>())
                {
                    enemies[i].GetComponent<GroundPatrol>().state = GroundPatrol.State.Dead;
                }
                else
                {
                    enemies[i].GetComponent<Reaper>().KillMe(gameObject);
                }
            }
        }
    }

    public void SetDefaultState()
    {
        state = State.Idle;
    }

    /// <summary>
    /// Отрисовывает радиус атаки на сцене,
    /// при выделении объекта
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    void FixedUpdate()
    {
        //if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        //Если коллайдер гг дотронулся до маски "Ground", то мы на земле
        //isGrounded = collider.IsTouchingLayers(ground); // исправить
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
    }

    /// <summary>
    /// Метод считывает нажатую клавишу движения в сторону
    /// </summary>
    void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
            isMovement = true;
            PlayRunSound();

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMovement = false;
        }

    }

    //Метод обрабатывает прыжок
    void Jump()
    {
        rb.AddForce(transform.up * 8f, ForceMode2D.Impulse);
        soundManager.PlayJumpSound();
    }

    /// <summary>
    /// Подсчет текущего состояния
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
            this.RecountLife(-deltaLife);
            isGodMod = true;
            Invoke("TurnOffGodeMode", 2f);
        }
    }

    /// <summary>
    /// Нанести персу урон
    /// </summary>
    /// <param name="damage">количество урона</param>
    public void SetDamage(int damage)
    {
        this.RecountLife(-damage);
    }

    private void TurnOffGodeMode()
    {
        isGodMod = false;
    }

    //Метод подсчитывает кол-во жизни, на основании получения урона запускает корутину и инициирует смерть 
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
        if (deltaLife < 0)
        {
            if (CurrentCountBlinks <= 0)
            {
                StopCoroutine(Blink());
                isHit = true;
                CurrentCountBlinks = CountBlinks;
                StartCoroutine(Blink());
            }
        }
        if (life <= 0)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            Invoke("Lose", 2f);
            state = State.Dead;

        }
    }

    //Метод вызова моментальной смерти через тег 
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
    //Метод моментальной смерти
    public void KillPerson()
    {
        SetDamage(life);
    }
    //Корутина изменения звета при получении урона игроком
    IEnumerator Blink()
    {
        float deltaAlpha = 4f / timeOfOneBlink;
        if (isHit)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, GetComponent<SpriteRenderer>().color.a - deltaAlpha);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, GetComponent<SpriteRenderer>().color.a + deltaAlpha);
        }
        //print(GetComponent<SpriteRenderer>().color.a);
        //print(1);
        if (GetComponent<SpriteRenderer>().color.a >= 1f)
        {
            isHit = true;
            CurrentCountBlinks--;
        }
        else if (GetComponent<SpriteRenderer>().color.a <= 0)
        {
            isHit = false;
        }
        if (CurrentCountBlinks <= 0)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(Blink());
    }
    //Метод перезапуска сцены 
    public void Lose()
    {
        main.gameObject.GetComponent<Main>().Lose();
    }
    //Метод сбора души с звуком 
    public void SoulCount()
    {
        soulsCount++;
        soundManager.PlayTakeItemsSound();
    }
    //Метод исцеления с звуком 
    public void LifeCount()
    {
        RecountLife(10);
        soundManager.PlayHillSound();
    }
    //Метод вызова звука бега 
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
   
    //Метод передачи данных в счетчик 
    public int GetCountUI()
    {
        return soulsCount;
        //return life;
    }
    //Метод включения подката
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
    //Метод отключения подката
    private void OffRollover()
    {
        speed -= speedRollover;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        //state = State.Idle;
        onRollover = false;
        if (isGrounded && state != State.Crushed && state != State.SawingInRollover)
        {
            state = State.Idle;
        }
    }
    //Метод вызова звука бега при срабаывании события на анимации
    private void soundsRunControl()
    {
        int randomInt = Random.Range(0, soundsRun.Length);
        audioSource.PlayOneShot(soundsRun[randomInt]);
    }
}




