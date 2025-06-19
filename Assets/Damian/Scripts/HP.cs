using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float hp;

    public int startlvl;

    public int goldgain;

    public Slider hpSlider;
    public Image fillImage;

    private void OnEnable()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    public void TakeDamage(float dmg)
    {
        float diff = gameState.GetDifficultyMultiplier();
        
        int scaling = gameState.level - startlvl;
        if (scaling <= 1) scaling = 0;


        float reducedDmg = dmg * Mathf.Pow(0.9f, scaling); //a curved dmg reduction so lvl 1 10% lvl 2 19  lvl 3 27 might change values but not this maath bcs just no
        
        reducedDmg *= diff;

        hpSlider.gameObject.SetActive(true);
        hpSlider.value -= reducedDmg / 100;
        UpdateFillColor();

        hp -= Mathf.RoundToInt(reducedDmg);
        if (hp < 1)
        {
            Destroy(gameObject);
            gameState.gold += Mathf.RoundToInt(goldgain * diff);
        }
    }

    private void UpdateFillColor()
    {
        float t = hpSlider.normalizedValue;
        fillImage.color = Color.Lerp(Color.red, Color.green, t);
    }
}
