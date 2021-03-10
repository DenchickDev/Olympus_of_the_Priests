using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

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
    /// ���-�� ������
    /// </summary>
    [SerializeField]
    private int life = 100;

    /// <summary>
    /// ����� �����
    /// </summary>
    public LayerMask ground;

    private new Collider2D collider;

    /// <summary>
    /// ������� ���������������
    /// ����� � ������
    /// </summary>
    private bool isGrounded;

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
        /// �������
        /// </summary>
        Hurt,
        /// <summary>
        /// ������
        /// </summary>
        Dead
    }

    /// <summary>
    /// ������� ��������� ���������
    /// </summary>
    public State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
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
        //���� ��������� �� ���������� �� ����� "Ground", �� �� �� �����
        isGrounded = collider.IsTouchingLayers(ground);
    }

    /// <summary>
    /// ����� ��������� ������� ������� �������� � �������
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

    //����� ������������ ������
    void Jump()
    {
        rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
        state = State.Jumping;
    }

    /// <summary>
    /// ������� �������� ���������
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
}
