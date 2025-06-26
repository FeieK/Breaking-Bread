using UnityEngine;

public class hpRegen : MonoBehaviour
{
    //dash such a lifesaver
    public float hpincrease = 20f;
    public float duration = 5f;
    public float dashCooldown = 10f;

    private bool isDashing = false;
    private float timeLeft = 0f;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (!isDashing && cooldownTimer <= 0 && Input.GetKeyDown(KeyCode.H))
        {
            Startheal();
        }

        if (isDashing)
        {
            healing();
        }
    }


    void Startheal()
    {
        isDashing = true;
        timeLeft = duration;
        cooldownTimer = dashCooldown;
    }

    void healing()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            gameState.hpRecovery = 20;
        }
        else
        {
            isDashing = false;
        }
    }

}
