using UnityEngine;

public class BulletBoomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timer = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            rb.AddForce(transform.right * -5);
        }
    }
}
