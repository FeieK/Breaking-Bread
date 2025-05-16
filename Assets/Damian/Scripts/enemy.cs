using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections.Generic;



public class enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    public Pathfinding pathfinding;
    public PathGrid grid;

    private List<Node> path;
    private int targetIndex;
    public float hp;

    [System.Obsolete]
    private void Start()
    {

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (target == null) return;

        path = pathfinding.FindPath(transform.position, target.position);
        targetIndex = 0;
    }

    private void Update()
    {
        if (path == null || path.Count == 0) return;

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
     public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp < 1)
        {
            Destroy(gameObject);
        }
    }
}



