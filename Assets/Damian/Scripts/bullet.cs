using UnityEngine;

public class bullet : MonoBehaviour
{
    public int dmg;
    public float Time;
    public bool Piercing;
    public bool RandomRotation;
    private void Awake()
    {
        Destroy(gameObject, Time);
    }

    private void Start()
    {
        RandomRot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP enemyHP = collision.gameObject.GetComponent<HP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(dmg);
            }

            if (!Piercing)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }

    private void RandomRot()
    {
        if (RandomRotation)
        {
            Transform trans = GetComponent<Transform>();
            trans.eulerAngles = new Vector3(0, 0, Random.Range(0, 360) );
        }
    }
}
