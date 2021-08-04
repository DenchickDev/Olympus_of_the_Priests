using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : MonoBehaviour, IKillable
{
    /// <summary>
    /// Первая точка патруля
    /// </summary>
    public Transform point1;

    /// <summary>
    /// Вторая точка патруля
    /// </summary>
    public Transform point2;

    /// <summary>
    /// Скорость, перемещения между точками
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Время ожидания возле точек
    /// </summary>
    public float waintTime = 2f;

    /// <summary>
    /// Признак: должен ли двигаться
    /// </summary>
    public bool isMove = true;
    
    /// <summary>
    /// Тип патрулирования
    /// </summary>
    public enum TypePatrol
    {
        /// <summary>
        /// Патруль по вертикали
        /// </summary>
        VerticalPatrol,

        /// <summary>
        /// Патруль по горизонтали
        /// </summary>
        HorizontalPatrol,
    }

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
        /// Смерть
        /// </summary>
        Dead
    }

    /// <summary>
    /// Текущее состояние
    /// </summary>
    public State state = State.Idle;

    /// <summary>
    /// Текущее тип патрулирования
    /// </summary>
    public TypePatrol typePatrol = TypePatrol.HorizontalPatrol;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }
        if(state == State.Dead)
        {
            isMove = false;
        }
        if (transform.position == point1.position)
        {
            Transform swapVal = point1;
            point1 = point2;
            point2 = swapVal;
            isMove = false;
            StartCoroutine(Wainting());
            
        }
        CalculateState();
        anim.SetInteger("stateAnim", (int)state);
    }

    /// <summary>
    /// Подсчет текущего состояния
    /// </summary>
    void CalculateState()
    {
        if (state != State.Dead)
        {
            if (isMove)
            {
                state = State.Running;
            }
            else
            {
                state = State.Idle;
            }
        }
    }

    IEnumerator Wainting()
    {
        yield return new WaitForSeconds(waintTime);
        isMove = true;
        if(typePatrol == TypePatrol.HorizontalPatrol)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }

    public void KillMe()
    {
        state = State.Dead;
        var enemyTrigger = GetComponentInChildren<EnemyTrigger>();
        if (enemyTrigger != null)
        {
            enemyTrigger.MakeNoDamaging();
        }
        Invoke("DestroyMe", 1f);
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
