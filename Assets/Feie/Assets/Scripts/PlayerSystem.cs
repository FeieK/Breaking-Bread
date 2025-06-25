using System;
using System.Collections;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public bool canMove;
    public float speed = 5f;
    public float knockbackStrength;
    public int health;
    public bool canGetHurt;
    public float damageReduction;

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private GameController gameController;
    private bool stopKnockback;

    private HeartUiElement heartUiElement;
    // Start is called before the first frame update
    void Start()
    {

        health = 100;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        heartUiElement = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<HeartUiElement>();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, 200);
        gameState.pHp = health;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove && gameController.roundIsActive == true && gameController.paused == false)
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
        if (stopKnockback)
        {
            rb.linearVelocity *= 0.9f;
        }
        if (health <= 0)
        {
            gameController.Die();
            health = 100;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            canGetHurt = false;
            canMove = false;
            ChangeHp(-5); //DEBUG
            StartCoroutine(Stun(5));
            Vector2 knockbackDirection = (transform.position - collision.transform.position);
            rb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);

        }
    }

    IEnumerator Stun(int maxTimes)
    {
        int interval = 0;
        while (interval < maxTimes)
        {
            if (interval == 2)
            {
                canMove = true;
                stopKnockback = false;
            }
            stopKnockback = true;
            sp.color = Color.red;
            sp.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sp.enabled = true;
            yield return new WaitForSeconds(0.1f);
            interval++;
        }
        sp.color = Color.white;
        canGetHurt = true;
    }

    public void ChangeHp(int deltaHp)
    {
        if (canGetHurt)
        {
            if (deltaHp < 0)
            {
                StartCoroutine(heartUiElement.HitEffectHeart());
                float damage = deltaHp - (deltaHp * damageReduction);
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


}
