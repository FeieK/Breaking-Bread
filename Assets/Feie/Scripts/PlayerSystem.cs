using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private GameController gameController;

    public bool canMove;
    public float speed = 5f;
    public float knockbackStrentch;
    public int health;
    public float damageReduction;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove && gameController.roundIsActive == true)
        {
            float speedHorizontal = Input.GetAxisRaw("Horizontal") * speed;
            float speedVertical = Input.GetAxisRaw("Vertical") * speed;
            if (speedHorizontal > 0)
            {
                sp.flipX = false;
            }
            if (speedHorizontal < 0)
            {
                sp.flipX = true;
            }
            rb.linearVelocity = new Vector2(speedHorizontal, speedVertical);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            canMove = false;
            StartCoroutine(Stun(5));
            Vector2 knockbackDirection = (transform.position - collision.transform.position);
            rb.AddForce(knockbackDirection * knockbackStrentch, ForceMode2D.Impulse);
        }
    }

    IEnumerator Stun(int maxTimes)
    {
        int interval = 0;
        while (interval < maxTimes)
        {
            sp.color = Color.red;
            sp.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sp.enabled = true;
            yield return new WaitForSeconds(0.1f);
            interval++;
        }
        rb.linearVelocity = Vector2.zero;
        canMove = true;
        sp.color = Color.white;
    }

    void ChangeHp(int deltaHp)
    {
        if (deltaHp < 0)
        {
            float damage = deltaHp - deltaHp * damageReduction;
            health += (int)Math.Round(damage);
        }
        else
        {
            health += deltaHp;
        }
        if (health <= 0)
        {
            gameController.Die();
        }
    }

}
