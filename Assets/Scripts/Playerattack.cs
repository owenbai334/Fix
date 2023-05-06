using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerattack : MonoBehaviour
{
    public int damage;
    private Animator animator;
    private PolygonCollider2D polyGonCollider2D;
    public float attackdelay;
    public float attack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        polyGonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Melee");
            StartCoroutine(StartAttack());
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackdelay);
        polyGonCollider2D.enabled = true;
        StartCoroutine(Attacking());
    }
    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attack);
        polyGonCollider2D.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().EnemyTakeDamage(damage);
        }
    }
}