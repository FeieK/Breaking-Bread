using UnityEngine;

public class shield : MonoBehaviour
{
    //makes a shield infront of u or wel activates it
    public GameObject shieldprefab;
    public bool shieldActive = false;
    public float shieldDuration = 3f;
    public float shieldCooldown = 5f;

    private float shieldTimer = 0f;
    private float shieldCooldownTimer = 0f;

    void Update()
    {
        if (shieldCooldownTimer > 0)
            shieldCooldownTimer -= Time.deltaTime;

        if (!shieldActive && shieldCooldownTimer <= 0 && Input.GetKeyDown(KeyCode.F)) 
        {
            ActivateShield();
        }

        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                DeactivateShield();
            }
        }
    }

    void ActivateShield()
    {
        shieldActive = true;
        shieldprefab.SetActive(true);
        shieldTimer = shieldDuration;
        shieldCooldownTimer = shieldCooldown;
    }

    void DeactivateShield()
    {
        shieldprefab.SetActive(false);
        shieldActive = false;
    }

}
