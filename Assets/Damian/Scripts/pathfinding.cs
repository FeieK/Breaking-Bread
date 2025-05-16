using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public PathGrid grid;

    public List<Node> lastPath;


    public List<Node> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                lastPath = RetracePath(startNode, targetNode);
                return lastPath;
                //to see path else 
                //return RetracePath(startNode, targetNode);
            }
            // Checks walkable neighbors
            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }

            }
        }

        return null;
    }


    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    //distence calculations
    int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }

    private void OnDrawGizmos()
    {
        if (lastPath != null)
        {
            for (int i = 0; i < lastPath.Count; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(lastPath[i].worldPosition, Vector3.one * (grid.nodeDiameter * 0.9f));

                if (i > 0)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(lastPath[i - 1].worldPosition, lastPath[i].worldPosition);
                }
            }
        }
    }
}
