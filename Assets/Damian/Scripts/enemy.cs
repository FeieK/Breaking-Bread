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

    [System.Obsolete]
    private void OnEnable()
    {
        Player = GameObject.Find("player(Clone)")?.transform;

        GameObject script = GameObject.Find("Map");
        pathfinding = script.GetComponent<Pathfinding>();
        grid = script.GetComponent<PathGrid>();

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
        if (path == null || path.Count == 0)
        {
            Vector2 moveTo = ((Vector2)Player.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(moveTo * speed * Time.deltaTime);
        }
        else
        {

                Vector2 currentWaypoint = path[targetIndex].worldPosition;
                if (Vector2.Distance(transform.position, currentWaypoint) < 0.1f)
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



