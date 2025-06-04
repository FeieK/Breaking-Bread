using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections.Generic;



public class enemy : MonoBehaviour
{
    private Transform Player;
    private float speed = 5f;

    private Pathfinding pathfinding;
    private PathGrid grid;

    private List<Node> path;
    private int targetIndex;

    private bool hitplayer;

    public LayerMask obstacleLayer;
    public LayerMask PlayerleLayer;

    public float playerCheckRadius = 1.5f;

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
        hitplayer = Physics2D.OverlapCircle(transform.position, playerCheckRadius, PlayerleLayer);

        if (hitplayer)
        {
           
        }
        else
        {
            
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
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
    }

}



