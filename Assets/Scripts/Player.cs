using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    public float jumplimit;
    public GameObject Bullet;
    public float fireRate = 0.5f;
    Rigidbody2D rb2d;
    static public bool Facingright;
    int jumpcount;
    public Animator animator;
    public int m_sec;
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpcount = 0;
        Facingright = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Run();
        Fall();
        Falltwo();
    }

    void Move()
    {  
        if (Input.GetButtonDown("Jump") && jumpcount <2)
        {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpspeed );
            animator.SetBool("IsJumping", true);
            if (Input.GetButtonDown("Jump") && jumpcount>=1)
            {
                animator.SetBool("IsJumpingII", true);
                animator.SetBool("IsJumping", false);
            }
            jumpcount += 1;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && Facingright == false|| Input.GetAxisRaw("Horizontal") < 0 && Facingright)
        {
            Flip();
        }  
        
        rb2d.velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal"), rb2d.velocity.y);      
    }

   void OnCollisionEnter2D (Collision2D col)
    {
       if (col.gameObject.CompareTag("ground"))
        {
            OnLandEvent.Invoke();
            jumpcount = 0;
         }
    }
    void Flip()
    {
        Facingright = !Facingright;
        transform.Rotate(0,180,0);
    }
    void Run()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
    public void OnLanding () 
    {
        animator.SetBool("FallingII", false);
        animator.SetBool("Falling", false);
    }
    void Fall()
    {
        if (rb2d.velocity.y < 0.0f)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("Falling", true);
        }
    }
    void Falltwo()
    {
        if (rb2d.velocity.y < 0.0f&& jumpcount >= 2)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsJumpingII", false);
            animator.SetBool("FallingII", true);
        }
    }
}
