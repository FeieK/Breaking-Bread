using UnityEngine;

public class HP : MonoBehaviour
{
    public float hp;

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp < 1)
        {
            Destroy(gameObject);
        }
    }
}
