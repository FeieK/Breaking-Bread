using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    public bool shooting;
    bool readyToShoot;
    bool reloading;

    //Reference
    public Transform attackPoint;
    private Transform rotatePoint;
    private Transform parent;
    private Transform player;
    private Transform map;

    //Bug fixing
    public bool allowInvoke;

    //targetpoint
    Vector3 targetPoint;

    //ai
    public bool ai = false;

    //UI
    private TextMeshProUGUI ammo;
    private Slider ReloadBar;

    private void Awake()
    {
        bulletsLeft = magSize;
        readyToShoot = true;

        parent = this.transform.parent;
        rotatePoint = parent.parent;
        player = rotatePoint.transform.parent;
        map = player.transform.parent;

    }

    private void Update()
    {
        if (ai)
        {
            EnemyAIInput();
        }
        else
        { 
            MyInput();
            ReloadUI();
            AmmoUI();
        }
        Flip();
    }
    private void EnemyAIInput()
    {

        //set shooting to true

        // Auto-reload logic
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        // Shooting logic
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
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

        if (!ai)
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            targetPoint = transform.position + transform.right * 10f;
        }

        //calc spread
        float x = Random.Range(-spread, spread);



        //instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity, map.transform);

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

    private void ReloadUI()
    {
        if (ReloadBar != null)
        {
            if (reloading)
            {
                ReloadBar.gameObject.SetActive(true);
                ReloadBar.maxValue = reloadTime;
                ReloadBar.value += Time.deltaTime;
            }
            else
            {
                ReloadBar.value = 0;
                ReloadBar.gameObject.SetActive(false);
            }
        }
        else
        {
            ReloadBar = GameObject.FindWithTag("ReloadUI").GetComponent<Slider>();
        }
    }

    private void AmmoUI()
    {
        if (ammo != null)
        {
            ammo.text = (bulletsLeft / bulletsPerInput).ToString() + " / " + (magSize / bulletsPerInput).ToString();
        }
        else
        {
            ammo = GameObject.FindWithTag("AmmoUI").GetComponent<TextMeshProUGUI>();
        }
    }
}
