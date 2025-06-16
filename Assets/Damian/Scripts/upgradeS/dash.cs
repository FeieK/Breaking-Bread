using UnityEngine;

public class dash : MonoBehaviour
{
    //dash such a lifesaver
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float dashCooldownTimer = 0f;
    private Vector2 dashDirection;

    void Update()
    {
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (!isDashing && dashCooldownTimer <= 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }

        if (isDashing)
        {
            DashMovement();
        }
    }


    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownTimer = dashCooldown;

        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.right;
    }

    void DashMovement()
    {
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            isDashing = false;
        }
    }

}
