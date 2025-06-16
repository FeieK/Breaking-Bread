using UnityEngine;

public class powerShot : MonoBehaviour
{
    //strong bullet on cooldown w that oneshots 
    public GameObject powerBullet;

    public float chargeTimeRequired = 2f;
    private float chargeTimer = 0f;
    private bool isCharging = true;

    public Transform firePoint;


    void Update()
    {

            chargeTimer += Time.deltaTime;

        //mouse 2
        if (Input.GetButtonDown("Fire2"))
        {
            if (chargeTimer >= chargeTimeRequired)
            {
                Shoot(powerBullet);
            }
            chargeTimer = 0f;
        }
    }


    void Shoot(GameObject bulletPrefab)
    {

        GameObject currentBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, this.transform.parent);
        //add forces to bullet
        currentBullet.transform.rotation = firePoint.rotation;
        currentBullet.transform.Rotate(0, 0, 0);
        currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * 10 , ForceMode2D.Impulse);
    }

}
