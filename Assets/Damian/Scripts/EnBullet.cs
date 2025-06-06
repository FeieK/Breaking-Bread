using UnityEngine;

public class EnBullet : MonoBehaviour
{
    public int dmg;
    public float Time;
    public bool Piercing;
    public bool meele;

    private void Awake()
    {
        Destroy(gameObject, Time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerSystem PlayerHP = collision.gameObject.GetComponent<PlayerSystem>();
            if (PlayerHP != null)
            {
                PlayerHP.ChangeHp(-dmg);
            }

            if (!Piercing)
            {
                Destroy(gameObject);
            }
        }

        if (!meele)
        {
            if (collision.gameObject.CompareTag("Walls"))
            {
                Destroy(gameObject);
            }
        }
    }
}
