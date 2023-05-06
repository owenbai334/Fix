using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public int  time_int;
    public float attackwait;
    public int time;
    bool meet;
    int waittime;
    List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        bullets.Add(this.gameObject);
        InvokeRepeating(nameof(Timer), 1, 1);
        if (Player.Facingright!=true)
        {
            speed = -speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
         StartCoroutine(StartAttack());
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        if (time_int == 0)
        {
            animator.SetTrigger("BubbleBoom");
            StartCoroutine(Boom());
        }
        if (this.gameObject.CompareTag("CollisionBubble")|| this.gameObject.CompareTag("Match 3"))
        {
            meet = true;
        }
        if (meet)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (waittime - time_int <0 && waittime == 0 )
            {
                waittime = time_int;
            }
        }
        if (bullets.Count >= 3)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Destroy(bullets[i]);
            }
        }

    }
    public void Flip(bool Facingright)
    {
        if (Facingright== false)
        {
            speed = -speed ;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("Success");
            this.gameObject.tag = "CollisionBubble";
            col.gameObject.tag = "CollisionBubble";
            bullets.Add(col.gameObject);
        }
        if(this.gameObject.CompareTag("CollisionBubble") && col.gameObject.CompareTag("CollisionBubble"))
        {
            if (waittime-time_int>0)
            {
             this.gameObject.tag = "Match 3";
             bullets.Add(col.gameObject);
             //col.transform.tag = "Match 3";
            }  
        }
        if (col.gameObject.CompareTag("finish"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackwait);
        animator.SetTrigger("FlyOut");
        if (meet)
        {
            yield break;
        }else
        {
            StartCoroutine(Float());
            yield break;
        }
    }
    IEnumerator Float()
    {
        yield return new WaitForSeconds(time);
        if (meet)
        {
         yield break;
        }
        else
        {
         animator.SetTrigger("Floating");
         speed = 0;
         GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
         yield break;
        }
    }
    void Timer()
    {
        time_int -= 1;
       
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
}
