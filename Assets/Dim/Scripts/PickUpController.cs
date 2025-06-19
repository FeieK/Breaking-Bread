using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    private Rigidbody2D rb;
    private Transform player;
    private Transform gunContainer;

    public float pickUpRange;

    public int wep;

    public bool equipped;
    public static bool slotFull;
    private GameObject map;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        map = GameObject.Find("Map");
        Transform rotatePoint = player.GetChild(0);
        gunContainer = rotatePoint.GetChild(0);

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
        //Check if player is in range and "Q" is pressed
        //changed this bcs its a pain to have ui and this on 1 key xdd
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.Q) && !slotFull)
        {
            PickUp();
        }
        else if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        gameState.eqwep = wep;
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

        //set parent to map to allow despawn
        transform.SetParent(map.transform);

        //Make RigidBody not kinematic and BoxCollider normal
        rb.bodyType = RigidbodyType2D.Dynamic;

        //gun carries momentum of player
        rb.linearVelocity = player.GetComponent<Rigidbody2D>().linearVelocity;
    }
}
