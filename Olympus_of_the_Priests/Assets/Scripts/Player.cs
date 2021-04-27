using System.Collections;
using System.Collections.Generic;
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
    /// </summary>
    [SerializeField]
    public SoundManager soundManager;

    /// <summary>
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

    /// <summary>
    /// Маска земли
    /// </summary>
    public LayerMask ground;

    private new Collider2D collider;

    /// <summary>
    /// Признак соприкосновения
    /// героя с землей
    /// </summary>
    private bool isGrounded;

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
        Sawing
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
        if ((/*Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||*/ Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Jump();
        }
        else if (state != State.Dead && state != State.Combustion && state !=State.Sawing && Input.GetMouseButtonDown(0) && onRollover == false)
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
        if ((Input.GetKeyDown(KeyCode.S)) && isGrounded)
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
        isGrounded = collider.IsTouchingLayers(ground); // исправить
    }

    /// <summary>
    /// Метод считывает нажатую клавишу движения в сторону
    /// </summary>
    void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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
        if (state != State.Dead && state != State.Combustion && state != State.Stab && state != State.Rollover && state != State.Sawing)
        {
            if (rb.velocity.y < -.1f)
            {
                state = State.Falling;
            }
            else if (rb.velocity.y > .1f)
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
        if (life <= 0 )
        {
            GetComponent<Rigidbody2D>().simulated = false;
            state = State.Dead;
            Invoke("Lose", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            SetDamageWithGodMode(life);
            GetComponent<Rigidbody2D>().simulated = false;
            state = State.Combustion;
            Invoke("Lose", 2f);
        }
        if(collision.gameObject.tag == "Saw")
        {
            SetDamageWithGodMode(life);
            GetComponent<Rigidbody2D>().simulated = false;
            state = State.Sawing;
            Invoke("Lose", 2f);
        }
      /*  if (collision.gameObject.tag == "Rock")
        {
            SetDamageWithGodMode(life);
            GetComponent<Rigidbody2D>().simulated = false;
            state = State.Combustion;
            Invoke("Lose", 2f);
        }*/
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
    public void Lose()
    {
        main.gameObject.GetComponent<Main>().Lose();
    }
    public void SoulCount()
    {
        soulsCount++;
        soundManager.PlayTakeItemsSound();
    }
    public void LifeCount()
    {
        RecountLife(10);
        soundManager.PlayHillSound();
    }
    public int GetCountUI()
    {
        return soulsCount;
        //return life;
    }
    private void OnRollover()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = true;
        state = State.Rollover;
        Invoke("OffRollover", timeRollover);
        onRollover = true;
    }
    private void OffRollover()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        state = State.Idle;
        onRollover = false;
    }
}




