using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    public LayerMask obstacleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    public float nodeDiameter;
    int gridSizeX, gridSizeY;

    [Header("Prefabs")]

    public int EnemySpawns;
    
    public bool showGrid = false;
    
    public GameObject wall;
    public GameObject door;
    public List<GameObject> Object;
    public List<GameObject> Enemy;
    public GameObject Player;


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        PlaceObjects();
        UpdateWalkableNodes();
    }

    // Creates the grid
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    void PlaceObjects()
    {
        Vector2Int doorPosition = new Vector2Int(gridSizeX / 2, 0);
        List<Node> candidateNodes = new List<Node>();


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Node node = grid[x, y];

                if (x == 0 || y == 0 || x == gridSizeX - 1 || y == gridSizeY - 1)
                {
                    if (x == doorPosition.x && y == doorPosition.y)
                    {
                        Instantiate(door, node.worldPosition, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(wall, node.worldPosition, Quaternion.identity);
                    }
                }
                else
                {
                    if (node.walkable)
                    {
                        candidateNodes.Add(node); // Stores it for other placements
                    }
                }
            }
        }

        if (candidateNodes.Count > 0)
        {
            //spawns player
            int playerIndex = Random.Range(0, candidateNodes.Count);
            Node playerNode = candidateNodes[playerIndex];
            candidateNodes.RemoveAt(playerIndex);
            Instantiate(Player, playerNode.worldPosition, Quaternion.identity);

            //spawns objects
            Node randomNode = candidateNodes[Random.Range(0, candidateNodes.Count)];
            int placeCount = Mathf.Min(Object.Count, candidateNodes.Count);
            for (int i = 0; i < placeCount; i++)
            {
                int index = Random.Range(0, candidateNodes.Count);
                Node chosen = candidateNodes[index];
                candidateNodes.RemoveAt(index);

                GameObject randomPrefab = Object[Random.Range(0, Object.Count)];
                Instantiate(randomPrefab, chosen.worldPosition, Quaternion.identity);
            }

            //spawns enemy
            for (int i = 0; i < EnemySpawns && candidateNodes.Count > 0; i++)
            {
                int enemyIndex = Random.Range(0, candidateNodes.Count);
                Node enemyNode = candidateNodes[enemyIndex];
                candidateNodes.RemoveAt(enemyIndex);

                GameObject enemyPrefab = Enemy[Random.Range(0, Enemy.Count)];
                Instantiate(enemyPrefab, enemyNode.worldPosition, Quaternion.identity);
            }

        }
    }

    void UpdateWalkableNodes()
    {
        foreach (Node node in grid)
        {
            node.walkable = !Physics2D.OverlapCircle(node.worldPosition, nodeRadius, obstacleMask);
        }
    }

    // Finds the node
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.Clamp(Mathf.RoundToInt((gridSizeX - 1) * percentX), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt((gridSizeY - 1) * percentY), 0, gridSizeY - 1);

        return grid[x, y];
    }

    // Gets neighbors of a given node for pathfinding
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        foreach (var dir in directions)
        {
            int checkX = node.gridX + dir.x;
            int checkY = node.gridY + dir.y;

            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
            {
                neighbors.Add(grid[checkX, checkY]);
            }
        }

        return neighbors;
    }

    // shows the grid
    private void OnDrawGizmos()
    {
        if (!showGrid) return;

        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter * 0.9f));
            }
        }
    }
}
