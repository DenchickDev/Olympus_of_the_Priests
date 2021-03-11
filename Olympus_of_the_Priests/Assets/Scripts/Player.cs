using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    bool isHit = false;
    public Main main;

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
    ///  Максимальное кол-во жизней 
    /// </summary>
    [SerializeField]
    private int life;
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
        /// Ранение
        /// </summary>
        Hurt,
        /// <summary>
        /// Смерть
        /// </summary>
        Dead
    }

    /// <summary>
    /// Текущее состояние персонажа
    /// </summary>
    public State state = State.Idle;

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
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Jump();
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
        print((int)state);
        
    }
    void FixedUpdate()
    {
        //if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        //Если коллайдер гг дотронулся до маски "Ground", то мы на земле
        isGrounded = collider.IsTouchingLayers(ground);
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
        rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
        state = State.Jumping;
    }

    /// <summary>
    /// Подсчет текущего состояния
    /// </summary>
    void CalculateState()
    {
        //if (state == State.Jumping)
        //{
        //}
        if (rb.velocity.y < 0f)
        {
            state = State.Falling;
        }else if (rb.velocity.y > 0f)
        {
            state = State.Jumping;
        }
        else if (state == State.Falling)
        {
            if(rb.velocity.y >= -.1f && collider.IsTouchingLayers(ground))
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
    //Метод подсчитывает кол-во жизни, на основании получения урона запускает корутину и инициирует смерть 
    public void RecountLife(int deltaLife)
    {
        life = life + deltaLife;
        print(life);
        if (deltaLife <0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit()); 
        }
        if (life <= 0)
        {
            GetComponent <Rigidbody2D>().simulated = false;
            GetComponent<Animator>().SetBool("Player_Death", true);
            Invoke("Lose", 2f);

        }
        
    }
    //Корутина изменения звета при получении урона игроком
    IEnumerator OnHit()
    {
        if (isHit)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.08f, GetComponent<SpriteRenderer>().color.b - 0.08f);
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.08f, GetComponent<SpriteRenderer>().color.b + 0.08f);
        }
        if(GetComponent<SpriteRenderer>().color.g <= 1)
        {
            StopCoroutine(OnHit());
        }
        if(GetComponent<SpriteRenderer>().color.g <=0)
        {
            isHit = false;
        }
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }
    void Lose()
    {
        main.GetComponent<Main>().Lose();
        
    }
}   

