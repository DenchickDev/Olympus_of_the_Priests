using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    CapsuleCollider2D capsuleCollider2D;
    public Main main;
    public int soulsCount = 0;
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
    public StateSystem stateSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        //life = MaxLife;
        timeOfOneBlink = timeBlinking / CountBlinks;
        //Application.targetFrameRate = 7;

        actionButtons = new ActionButtonsPlayer();
        stateSystem = new StateSystem();
    }

    // Update is called once per frame
    void Update()
    {
        if(stateSystem.isActiveSetState && stateSystem.state != State.Rollover)
        {
            if (actionButtons.CheckJump() && isGrounded)
            {
                Jump();
            }
            else if (actionButtons.CheckAttack())
            {
                stateSystem.state = State.Stab;
                soundManager.PlayHitSound();
            }
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
        print(stateSystem.state.ToString());
        CalculateState();
        anim.SetInteger("stateAnim", (int)stateSystem.state);
        //Debug.Log("ddddd");
        //if (state == State.Combustion || state == State.Jumping)
        //    print(state.ToString());
        //print(GetComponent<SpriteRenderer>().color.g);
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x + 100, transform.position.y, transform.position.z));
        
    }
    //Метод срабатывает сразу же после полного срабатывания метода Update
    private void LateUpdate()
    {
        if (actionButtons.CheckRollover() && isGrounded && stateSystem.isActiveSetState && !actionButtons.CheckJump())
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
        stateSystem.SetDefaultState();
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
        if (rb.velocity.y < -.1f && !isGrounded)
        {
            stateSystem.state = State.Falling;
        }
        else if (rb.velocity.y > .1f && stateSystem.state != State.Crushed)
        {
            stateSystem.state = State.Jumping;
        }
        else if (stateSystem.state == State.Falling)
        {
            if (rb.velocity.y >= -.1f)
            {
                stateSystem.state = State.Idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            stateSystem.state = State.Running;
        }
        else
        {
            stateSystem.state = State.Idle;
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
            //GetComponent<Rigidbody2D>().simulated = false;
            actionButtons.enableAllHard = false;
            Invoke("Lose", 2f);
            stateSystem.state = State.Dead;

        }
    }

    /// <summary>
    /// Загрузить данные по игроку
    /// из файла
    /// </summary>
    /// <returns>Удалось ли загрузить данные</returns>
    public bool LoadCharacter()
    {
        SaveData data = SaveLoad.LoadGame(); //Получение данных

        if (!data.Equals(null)) //Если данные есть
        {
            string sceneName = SceneManager.GetActiveScene().name;

            //Данные относятся к текущей сцене?
            if (sceneName != data.sceneName)
            {
                return false;
            }

            life = data.life;
            soulsCount = data.soulsCount;
            actionButtons.enableAllHard = true;
            stateSystem.SetDefaultState();

            transform.position = new Vector3(data.positionPlayer[0], data.positionPlayer[1], data.positionPlayer[2]);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Распилить персонажа
    /// </summary>
    public void SawPlayer()
    {
        if (stateSystem.state == State.Rollover)
        {
            stateSystem.state = State.SawingInRollover;
        }
        else
        {
            stateSystem.state = State.Sawing;
        }
        KillPerson();
    }

    /// <summary>
    /// Раздавить(камнем) персонажа
    /// </summary>
    public void CrashWithRockPlayer()
    {
        stateSystem.state = State.Crushed;
        KillPerson();
    }

    /// <summary>
    /// Сжечь персонажа
    /// </summary>
    public void combustPlayer()
    {
        stateSystem.state = State.Combustion;
        KillPerson();
    }

    public void KillWithPitWithSpikes()
    {
        stateSystem.state = State.PitWithSpikes;
        KillPerson();
    }

    //Метод вызова моментальной смерти через тег 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(stateSystem.isActiveSetState)
        {
            if (collision.gameObject.tag == "Lava")
            {
                combustPlayer();
            }
            else if (collision.gameObject.tag == "Saw")
            {
                SawPlayer();
            }
            else if (collision.gameObject.tag == "Rock" && isGrounded)
            {
                CrashWithRockPlayer();
            }
            else if (collision.gameObject.tag == "PitWithSpikes")
            {
                KillWithPitWithSpikes();
            }
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
        Color defaultColor = spriteRenderer.color;
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
                spriteRenderer.color = Color.Lerp(current, next, i);
                yield return new WaitForFixedUpdate();
            }
        }
        //Железно установим, что под конец альфа канал равен 1
        spriteRenderer.color = defaultColor;
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
        bool isLoad = LoadCharacter();
        if (!isLoad)
        {
            main.gameObject.GetComponent<Main>().Lose();
        }
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

        if (stateSystem.state == State.Running)
        {
            if (isMovement == true && isSoundRunPlay == false)
            {
                soundManager.PlayRunSound();
                isSoundRunPlay = true;
                isMovement = false;

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
        if (stateSystem.state != State.Rollover)
        {
            speed += speedRollover;
            boxCollider2D.enabled = false;
            capsuleCollider2D.enabled = true;
            stateSystem.state = State.Rollover;
            Invoke("OffRollover", timeRollover);
            soundManager.PlayWoundSound();
        }
    }
    //Метод отключения подката
    private void OffRollover()
    {
        speed -= speedRollover;
        boxCollider2D.enabled = true;
        capsuleCollider2D.enabled = false;
        //state = State.Idle;
        //onRollover = false;
        /*if (isGrounded && stateSystem.isActiveSetState && stateSystem.state != State.Stab)
        {
            stateSystem.state = State.Idle;
        }
        if(!isGrounded && stateSystem.state != State.Combustion)
        {
            stateSystem.state = State.Falling;
        }*/
        stateSystem.SetDefaultState();
    }
    //Метод вызова звука бега при срабаывании события на анимации
    private void soundsRunControl()
    {
        int randomInt = Random.Range(0, soundsRun.Length);
        audioSource.PlayOneShot(soundsRun[randomInt]);
    }
}




