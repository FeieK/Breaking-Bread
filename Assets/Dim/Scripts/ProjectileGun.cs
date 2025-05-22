using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileGun : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet Force
    public float shootForce;
    public float upwardForce;

    //Gun Stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;

    public int magSize;
    public int bulletsPerInput;
    public bool allowButtonHold;

    int bulletsLeft;
    int bulletsShot;

    //Bools
    bool shooting;
    bool readyToShoot;
    bool reloading;

    //Reference
    public Transform attackPoint;

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


        Vector3 targetPoint;
        targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(targetPoint);

        //Calculate direction from attackpoint to targetpoint
        Vector2 directionWithoutSpread = targetPoint - attackPoint.position;

        //calc spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calc new direction with spread
        Vector2 directionWithSpread = directionWithoutSpread + new Vector2(x, y);

        //instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        //add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

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

}
