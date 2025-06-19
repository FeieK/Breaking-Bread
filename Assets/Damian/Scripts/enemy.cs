using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Security.Cryptography;



public class enemy : MonoBehaviour
{
    private Transform Player;
    public float speed = 5f;

    private Pathfinding pathfinding;
    private PathGrid grid;

    private List<Node> path;
    private int targetIndex;

    private bool hitplayer;

    public LayerMask obstacleLayer;
    public LayerMask PlayerleLayer;

    public float playerCheckRadius = 1.5f;

    //gun
    private ProjectileGun enemyGun;


    private void OnEnable()
    {
        Player = GameObject.Find("Player(Clone)")?.transform;

        GameObject script = GameObject.Find("Map");
        pathfinding = script.GetComponent<Pathfinding>();
        grid = script.GetComponent<PathGrid>();

        //updates path every 0.5f
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);

        enemyGun = GetComponentInChildren<ProjectileGun>();

    }


    void UpdatePath()
    {
        if (Player == null) return;

        path = pathfinding.FindPath(transform.position, Player.position);
        targetIndex = 0;
    }

    private void Update()
    {
        if (Player == null) return;

        //checks the range
        bool playerInRange = Vector2.Distance(transform.position, Player.position) <= playerCheckRadius;

        bool playerInshootwalkRange = Vector2.Distance(transform.position, Player.position) <= playerCheckRadius + 2;

        //checks if theres a object
        Vector2 direction = (Player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToPlayer, obstacleLayer);
         //bcs why not
        Debug.DrawRay(transform.position, direction * distanceToPlayer, Color.red);

        //if it doesnt hit or is inrange of to player

        if (playerInshootwalkRange && !playerInRange || hit.collider != null)
        {
            walk();
            shoot();
        }
        else if (!playerInRange || hit.collider != null)
        {
            walk();
            enemyGun.shooting = false;
        }
        //shoot
        else
        {
            shoot();
        }
    }

    private void shoot()
    {
            enemyGun.shooting = true;
    }

    private void walk()
    {

            // Move directly or using path
            if (path == null || path.Count == 0)
            {
                Vector2 moveTo = ((Vector2)Player.position - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(moveTo * speed * Time.deltaTime);
            }
            else
            {
                Vector2 currentWaypoint = path[targetIndex].worldPosition;
                if (Vector2.Distance(transform.position, currentWaypoint) < 1.5f)
                {
                    targetIndex++;
                    if (targetIndex >= path.Count)
                    {
                        path = null;
                        return;
                    }
                }

                Vector2 wpdirection = ((Vector2)currentWaypoint - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(wpdirection * speed * Time.deltaTime);
            }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            PlayerSystem PlayerHP = collision.gameObject.GetComponent<PlayerSystem>();
            if (PlayerHP != null)
            {
                PlayerHP.ChangeHp(-20);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius + 2);

    }

}



