using UnityEngine;

public class EnBullet : MonoBehaviour
{
    public int dmg;
    public float Time;
    public bool Piercing;
    public bool meele;
    public bool RandomRotation;


    private void Awake()
    {
        Destroy(gameObject, Time);
        RandomRot();

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

    private void RandomRot()
    {
        if (RandomRotation)
        {
            Transform trans = GetComponent<Transform>();
            trans.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        }
    }
}
