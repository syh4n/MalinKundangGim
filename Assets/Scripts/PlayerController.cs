using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;
    

    [SerializeField]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator m_anim;

    private int extraJump;
    public int extraJumpValue;

    [SerializeField]
    private bool isJumping;

    private float curPos;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(moveInput);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        //Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput == 0)
        {
            m_anim.SetBool("isWalking", false);
        }

        if (moveInput != 0 && isGrounded)
        {
            m_anim.SetBool("isWalking", true);
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }


        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            curPos = transform.position.y;
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
            curPos = transform.position.y;
            rb.velocity = Vector2.up * jumpForce;


        }
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
            m_anim.SetBool("isJumping", false);
            m_anim.SetBool("isDown", false);
        }

        if (!isGrounded)
        {
            m_anim.SetBool("isDown", true);

            if (transform.position.y > curPos)
            {
                m_anim.SetBool("isJumping", true);
            }
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void SetIdle() 
    {

        moveInput = 0;
    }


}
