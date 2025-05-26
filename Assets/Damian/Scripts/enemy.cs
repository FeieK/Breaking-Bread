using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections.Generic;



public class enemy : MonoBehaviour
{
    public Transform Player;
    private float speed = 5f;

    public Pathfinding pathfinding;
    public PathGrid grid;

    private List<Node> path;
    private int targetIndex;

    private bool hitobj;

    public LayerMask obstacleLayer;
    private float obstacleCheckRadius = 1.5f;

    [System.Obsolete]
    private void OnEnable()
    {
        Player = GameObject.Find("Player(Clone)")?.transform;

        GameObject script = GameObject.Find("Map");
        pathfinding = script.GetComponent<Pathfinding>();
        grid = script.GetComponent<PathGrid>();

        //updates path every 0.5f
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }


    void UpdatePath()
    {
        if (Player == null) return;

        path = pathfinding.FindPath(transform.position, Player.position);
        targetIndex = 0;
    }

    private void Update()
    {
        hitobj = Physics2D.OverlapCircle(transform.position, obstacleCheckRadius, obstacleLayer);

        //if we wanne use a worse but raycastusing way
        //if (path == null || path.Count == 0 || !hitobj)
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

            Vector2 direction = ((Vector2)currentWaypoint - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
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
        //someone really wanted a raycast gizmo so here u go :D
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, obstacleCheckRadius);

        //smt to see the player and maybe shoot to it idk we stil havent decided on how we impliment it
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 5);

        //for idk longer range and looks cool raycasting
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 8);
        //oke im mean ik sorry but i choose to not use raycasting bcs there better ways or u just never read this hopefully
    }

}



