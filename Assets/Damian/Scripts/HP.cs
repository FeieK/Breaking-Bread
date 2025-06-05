using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float hp;

    public int goldgain;

    public Scrollbar hpSlider;

    public void TakeDamage(float dmg)
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.size -= dmg / 100;
        hp -= dmg;
        if (hp < 1)
        {
            Destroy(gameObject);
            gameState.gold += goldgain;
        }
    }
}
