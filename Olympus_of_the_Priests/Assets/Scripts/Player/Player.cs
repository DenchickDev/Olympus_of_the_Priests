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
    /// Продолжительность одного мигания в мс
    /// </summary>
    private float timeOfOneBlink = 15f;

    /// <summary>
    /// Кол-во миганий
    /// </summary>
    [SerializeField]
    private int CountBlinks = 4;

    /// <summary>
    /// Продолжительность мигания в сек
    /// </summary>
    [SerializeField]
    private float timeBlinking = 2f;

    /// <summary>
    /// Продолжительность год мода в сек
    /// </summary>
    [SerializeField]
    private float timeGodMode = 2f;

    /// /// <summary>
    /// Режим бессмертия
    /// </summary>
    [SerializeField]
    private bool isGodMod = false;

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
    //Метод срабатывает сразу же после полного срабатывания метода Update
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
        //собсна атака
        //возвращаем все колайдеры, которые попадуться
        //в радиус attackRange относительно позиции attackPos
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
            StartBlinking();
            this.RecountLife(-deltaLife);
            isGodMod = true;
            Invoke("TurnOffGodeMode", timeGodMode);
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

    /// <summary>
    /// Запускает мигание (отменяет существующее мигание)
    /// </summary>
    void StartBlinking()
    {
        StopCoroutine("Blinking");
        StartCoroutine("Blinking");
    }

    /// <summary>
    /// Корутина с миганием
    /// </summary>
    /// <returns></returns>
    IEnumerator Blinking()
    {
        float time = timeOfOneBlink / 2.0f; //Время на смену цвета секундах (от альфа 1 до афльфа 0 и наоборот)
        float rate = 1.0f / time;
        Color defaultColor = GetComponent<SpriteRenderer>().color;
        defaultColor.a = 1;
        Color next = defaultColor;
        Color current = defaultColor;
        current.a = 0;

        //Одно мигание состоит из двух частей:
        //убрать альфа канал, потом добавить его, поэтому *2
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
        //Железно установим, что под конец альфа канал равен 1
        GetComponent<SpriteRenderer>().color = defaultColor;
        yield break;
    }

    /// <summary>
    /// Поменять местами color1 и color2
    /// </summary>
    /// <param name="color1">Первый цвет</param>
    /// <param name="color2">Второй цвет</param>
    void SwapColors(ref Color color1, ref Color color2)
    {
        Color temp;
        temp = color1;
        color1 = color2;
        color2 = temp;
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
        if (isGrounded && state != State.Crushed && state != State.Dead && state != State.SawingInRollover && state != State.Stab)
        {
            state = State.Idle;
        }
        if(!isGrounded && state !=State.Combustion)
        {
            state = State.Falling;
        }
    }
    //Метод вызова звука бега при срабаывании события на анимации
    private void soundsRunControl()
    {
        int randomInt = Random.Range(0, soundsRun.Length);
        audioSource.PlayOneShot(soundsRun[randomInt]);
    }
}




