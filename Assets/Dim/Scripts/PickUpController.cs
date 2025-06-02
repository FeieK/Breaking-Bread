using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody2D rb;
    private Transform player;
    private Transform gunContainer;

    public float pickUpRange;

    public bool equipped;
    public static bool slotFull;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Transform rotatePoint = player.GetChild(0);
        gunContainer = rotatePoint.GetChild(0);
    }

    private void OnEnable()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (equipped)
        {
            gunScript.enabled = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //enable script
        gunScript.enabled = true;

        //Make weapon child of player and move to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody kinematic and BoxCollider a trigger
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //disable script
        gunScript.enabled = false;

        //set parent to null
        transform.SetParent(null);

        //Make RigidBody not kinematic and BoxCollider normal
        rb.bodyType = RigidbodyType2D.Dynamic;

        //gun carries momentum of player
        rb.linearVelocity = player.GetComponent<Rigidbody2D>().linearVelocity;
    }
}
