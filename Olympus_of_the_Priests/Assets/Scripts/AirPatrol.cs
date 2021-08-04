using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : MonoBehaviour, IKillable
{
    /// <summary>
    /// ������ ����� �������
    /// </summary>
    public Transform point1;

    /// <summary>
    /// ������ ����� �������
    /// </summary>
    public Transform point2;

    /// <summary>
    /// ��������, ����������� ����� �������
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// ����� �������� ����� �����
    /// </summary>
    public float waintTime = 2f;

    /// <summary>
    /// �������: ������ �� ���������
    /// </summary>
    public bool isMove = true;
    
    /// <summary>
    /// ��� ��������������
    /// </summary>
    public enum TypePatrol
    {
        /// <summary>
        /// ������� �� ���������
        /// </summary>
        VerticalPatrol,

        /// <summary>
        /// ������� �� �����������
        /// </summary>
        HorizontalPatrol,
    }

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
        Dead
    }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public State state = State.Idle;

    /// <summary>
    /// ������� ��� ��������������
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
    /// ������� �������� ���������
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
