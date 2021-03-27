using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour
{
    /// <summary>
    /// �������� ��������
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// ������ ���� ��������������
    /// </summary>
    public float RadiusOfPatrol = 5f;

    /// <summary>
    /// ����� ���� ��������������
    /// </summary>
    public Transform point;

    /// <summary>
    /// �������� �� ������
    /// </summary>
    private bool _moveingRight;

    /// <summary>
    /// �������� �� ������
    /// </summary>
    public bool moveingRight
    {
        get
        {
            return _moveingRight;
        }
        set
        {
            if (value != _moveingRight)
            {
                isMove = false;
                StartCoroutine(Wainting());
            }
            _moveingRight = value;
        }
    }

    /// <summary>
    /// ����� �������� �����
    /// ������� ���� ��������������
    /// </summary>
    public float waintTime = 2f;

    /// <summary>
    /// �������: ������ �� ���������
    /// </summary>
    public bool isMove = true;


    Rigidbody2D rb;

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

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        Chill();
        CalculateState();
        anim.SetInteger("stateAnim", (int)state);
        //print(rb.velocity.x);
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

    /// <summary>
    /// ����� ��������������, ������/�����
    /// </summary>
    void Chill()
    {
        if (isMove)
        {
            //������� ������� �������������� + ����� NPC = ���� ������.
            if (moveingRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            //������� ������� �������������� - ����� NPC = ���� �������.
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
        }
    }

    /// <summary>
    /// ���������� ����������� ��������
    /// </summary>
    private void CheckDirection()
    {
        // �������� ��������.
        if (transform.position.x > point.position.x + RadiusOfPatrol)
        {
            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - RadiusOfPatrol)
        {
            moveingRight = true;
        }
    }

    IEnumerator Wainting()
    {
        yield return new WaitForSeconds(waintTime);
        isMove = true;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

    }

    /// <summary>
    /// ������������ ������ ����� �� �����,
    /// ��� ��������� �������
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (point != null)
        {
            Vector3 pointPatrol1 = new Vector3(point.position.x - RadiusOfPatrol, point.position.y, point.position.z);
            Vector3 pointPatrol2 = new Vector3(point.position.x + RadiusOfPatrol, point.position.y, point.position.z);
            Gizmos.DrawLine(pointPatrol1, pointPatrol2);
        }
        //Gizmos.DrawWireSphere(point.position, attackRange);
    }

}
