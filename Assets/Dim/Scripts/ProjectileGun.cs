using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ProjectileGun : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet Force
    public float shootForce;

    //Gun Stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;

    public int magSize;
    public int bulletsPerInput;
    public bool allowButtonHold;
    public bool shouldFlip;

    int bulletsLeft;
    int bulletsShot;

    //Bools
    bool shooting;
    bool readyToShoot;
    bool reloading;

    //Reference
    public Transform attackPoint;
    private Transform rotatePoint;

    //Bug fixing
    public bool allowInvoke;

    private void Awake()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        Flip();
    }

    private void MyInput()
    {
        //Check if hold input allowed
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else 
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
        {
            Reload();
        }
        //auto reload if out of ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }


        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //calc spread
        float x = Random.Range(-spread, spread);

        //instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        //add forces to bullet
        currentBullet.transform.rotation = rotatePoint.rotation;
        currentBullet.transform.Rotate(0, 0, x);
        currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * shootForce * Random.Range(0.9f, 1.1f), ForceMode2D.Impulse);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function, with timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerInput && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }

    private void Flip()
    {
        Transform parent = this.transform.parent;
        rotatePoint = parent.parent;

        if (shouldFlip)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();

            if (rotatePoint.rotation.z > 0.7f || rotatePoint.rotation.z < -0.7f)
            {
                spriteRend.flipY = true;
            }
            else
            {
                spriteRend.flipY = false;
            }
        }
    }
    //
}
