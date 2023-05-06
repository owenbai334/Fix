using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float moveDistance;
    public float flashTime;
    private SpriteRenderer sr;
    private Color originalColor;
    Vector2 thisPosition;
    Vector2 targetPosition;
    bool canReturn = false;

    // Start is called before the first frame update
    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        thisPosition = transform.position;
        targetPosition = new Vector2(transform.position.x + moveDistance, transform.position.y);
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (canReturn)
        {
            transform.position = Vector2.MoveTowards(transform.position, thisPosition, speed * Time.deltaTime);
            if (transform.position.x == thisPosition.x && transform.position.y == thisPosition.y)
            {
                canReturn = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                canReturn = true;
            }
        }
    }
    public void EnemyTakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
    }
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }
}
