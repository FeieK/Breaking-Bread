using UnityEngine;

public class bullet : MonoBehaviour
{
    public int dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP enemyHP = collision.gameObject.GetComponent<HP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(dmg);
            }
        
            Destroy(gameObject.transform.parent.gameObject);
        }

    }
}
