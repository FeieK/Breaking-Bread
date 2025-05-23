using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    public bool gameOver;
    public int stage;
    public SpriteRenderer[] srs;

    private Rigidbody2D[] rbs;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody2D>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        stage = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver)
        {
            if (stage == 0)
            {
                foreach (Rigidbody2D rb in rbs)
                {
                    if (rb.transform.position.x.ToString().Contains("-"))
                    {
                        rb.AddForce(Vector2.right * 200 * Time.deltaTime);
                    }
                    else
                    {
                        rb.AddForce(Vector2.left * 200 * Time.deltaTime);
                    }
                }
            }
            if (stage == 1)
            {
                foreach (Rigidbody2D rb in rbs)
                {
                    if (rb.transform.position.x.ToString().Contains("-"))
                    {
                        rb.AddForce(Vector2.left * 200 * Time.deltaTime);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * 200 * Time.deltaTime);
                    }
                }
            }
            if (stage == 2)
            {
                foreach (Rigidbody2D rb in rbs)
                {
                    rb.linearVelocity = Vector2.zero;
                }
                foreach (SpriteRenderer sr in srs)
                {
                    sr.enabled = false;
                }
                for (int i = 0; i < transform.childCount; i++)
                {

                    if (transform.GetChild(i).localPosition.x.ToString().Contains("-"))
                    {
                        transform.GetChild(i).localPosition = new Vector2(-20, 0);
                    }
                    else
                    {
                        transform.GetChild(i).localPosition = new Vector2(20, 0);
                    }
                }
                gameOver = false;
                stage = 0;
            }
        }
    }

    public void PlaySound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.2f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
