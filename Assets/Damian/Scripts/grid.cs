using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PathGrid : MonoBehaviour
{
    //for the grid and pathfinding
    public LayerMask obstacleMask;
    private Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    //grid and node size
    private float nodeDiameter;
    int gridSizeX, gridSizeY;

    //gues
    public RawImage minimap;

    //if showgrid
    public bool showGrid = false;

    //the enemys and all other prefabs
    [Header("Prefabs")]
    public List<obj> wall;
    public List<obj> door;
    public List<obj> objects;
    public List<obj> hubobjects;
    public List<obj> Enemy;
    public List<obj> Player;

    //keeps trag of player and enemie for respawn
    private GameObject spawnedPlayer;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Vector3 playerSpawnPosition;
    private List<Vector3> enemySpawnPositions = new List<Vector3>();
    private List<int> spawnedEnemyTypes = new List<int>();



    //when it enebles it makes a random map
    private void OnEnable()
    {
        gridWorldSize = new Vector2(Random.Range(room.minsize, room.maxsize), Random.Range(room.minsize, room.maxsize));
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);



        //StartCoroutine(miniMap());

        CreateGrid();
        PlaceObjects();
        UpdateWalkableNodes();
    }

    //when it gets disabled it destroys the old map
    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        bool allEnemiesDead = true;

        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                allEnemiesDead = false;
                break;
            }
        }
        //unlocks room is all enemys are ded
        room.unlock = allEnemiesDead;

        //u die u respawn
        if (room.die)
        {
            room.die = false;
            Respawn();
        }
    }


    //makes the grid
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

    //placces the objects in the grid
    void PlaceObjects()
    {
        Vector2Int doorPosition = GetDoorPosition(gridSizeX, gridSizeY);

        //so it doesnt spawn in the same place as another object
        List<Node> freespawnloc = new List<Node>();
        PlaceWallsAndDoor(doorPosition, freespawnloc);

        if (freespawnloc.Count == 0) return;

        Node playerNode = SpawnPlayer(freespawnloc);
        SpawnEnemies(freespawnloc, playerNode);
        SpawnObjects(freespawnloc);
    }

    // gues
    void PlaceWallsAndDoor(Vector2Int doorPosition, List<Node> freespawnloc)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Node node = grid[x, y];

                if (x == 0 || y == 0 || x == gridSizeX - 1 || y == gridSizeY - 1)
                {
                    if (x == doorPosition.x && y == doorPosition.y)
                    {
                        obj doorObj = door[0];
                        Quaternion rotation = Quaternion.identity;


                        //if (x == 0)
                        //    rotation = Quaternion.Euler(0, 90, 0);
                        //else if (x == gridSizeX - 1)
                        //    rotation = Quaternion.Euler(0, -90, 0);
                        //else if (y == 0)
                        //    rotation = Quaternion.Euler(0, 0, 0);
                        //else if (y == gridSizeY - 1)
                        //    rotation = Quaternion.Euler(0, 180, 0);

                        Instantiate(doorObj.prefab, node.worldPosition, rotation, transform);
                    }
                    else
                    {
                        obj wallObj = wall[Random.Range(0, wall.Count)];
                        Quaternion rotation = Quaternion.identity;

                        if (y == 0) rotation = Quaternion.Euler(0, 0, 0);
                        else if (x == gridSizeX - 1) rotation = Quaternion.Euler(0, 0, 90);
                        else if (y == gridSizeY - 1) rotation = Quaternion.Euler(0, 0, 180);
                        else if (x == 0) rotation = Quaternion.Euler(0, 0, 270);

                        Instantiate(wallObj.prefab, node.worldPosition, rotation, transform);
                        node.walkable = false;
                    }
                }
                else if (node.walkable)
                {
                    freespawnloc.Add(node);
                }
            }
        }
    }

    Node SpawnPlayer(List<Node> freespawnloc)
    {
        //it takes a random note of freespawnloc then spawns it there and removes the loc of the list
        int index = Random.Range(0, freespawnloc.Count);
        Node node = freespawnloc[index];
        freespawnloc.RemoveAt(index);
        playerSpawnPosition = node.worldPosition;
        spawnedPlayer = Instantiate(Player[0].prefab, node.worldPosition, Quaternion.identity, transform);
        return node;
    }



    void SpawnObjects(List<Node> freespawnloc)
    {
        int attempts = 100;
        int spawned = 0;

        if (room.placehubworldobj)
        {
            //hub objects
            foreach (var toSpawn in hubobjects)
            {
                int retries = 100;
                bool placed = false;

                while (retries-- > 0 && freespawnloc.Count > 0)
                {
                    Node spawnplace = freespawnloc[Random.Range(0, freespawnloc.Count)];
                    if (TryPlaceObject(toSpawn, spawnplace, freespawnloc))
                    {
                        spawned++;
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    room.failsave = true;
                    //failsaves makes a new map but if it not fit fast then u stuck here or aperently it just faills
                    Debug.LogWarning($"Failed to place: {toSpawn.prefab.name} so increase mapsize or reduce object size map size is in room select");
                }
                else
                {
                    room.failsave = false;
                }
            }
        }
        else
        {
            //room placement
            while (spawned < room.placNumObj && freespawnloc.Count > 0 && attempts-- > 0)
            {
                obj toSpawn = objects[Random.Range(0, objects.Count)];
                Node spawnplace = freespawnloc[Random.Range(0, freespawnloc.Count)];

                if (TryPlaceObject(toSpawn, spawnplace, freespawnloc))
                {
                    spawned++;
                }
            }
        }
    }

    //see if it can rotate/not overlap with others
    bool TryPlaceObject(obj toSpawn, Node baseNode, List<Node> freespawnloc)
    {
        Quaternion rotation = Quaternion.identity;
        Vector2Int size = toSpawn.size;

        if (toSpawn.canRotate && Random.value > 0.5f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            size = new Vector2Int(size.y, size.x);
        }

        Vector2Int basePos = new Vector2Int(baseNode.gridX, baseNode.gridY);
        if (!IsPlacementValid(basePos, size, 2)) return false;
        //if its valid proceede
        for (int dx = 0; dx < size.x; dx++)
        {
            for (int dy = 0; dy < size.y; dy++)
            {
                Node n = grid[basePos.x + dx, basePos.y + dy];
                freespawnloc.Remove(n);
                n.walkable = false;
            }
        }

        Instantiate(toSpawn.prefab, baseNode.worldPosition, rotation, transform);
        return true;
    }



    bool IsPlacementValid(Vector2Int basePos, Vector2Int size, int buffer = 1)
    {
        Vector2 center = GridToWorld(basePos, size);
        Vector2 worldSize = new Vector2(size.x * nodeDiameter - 0.05f, size.y * nodeDiameter - 0.05f);

        // Make sure there's no physical collider
        Collider2D hit = Physics2D.OverlapBox(center, worldSize, 0f, obstacleMask);
        if (hit != null) return false;

        // Grid-based buffer check
        for (int dx = -buffer; dx < size.x + buffer; dx++)
        {
            for (int dy = -buffer; dy < size.y + buffer; dy++)
            {
                int x = basePos.x + dx;
                int y = basePos.y + dy;

                if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY)
                    return false;

                if (!grid[x, y].walkable)
                    return false;
            }
        }

        return true;
    }



    Vector2 GridToWorld(Vector2Int basePos, Vector2Int size)
    {
        Vector2 baseWorld = grid[basePos.x, basePos.y].worldPosition;

        float offsetX = (size.x - 1) * nodeDiameter / 2f;
        float offsetY = (size.y - 1) * nodeDiameter / 2f;

        return new Vector2(baseWorld.x + offsetX, baseWorld.y + offsetY);
    }


    //spawns enemies
    void SpawnEnemies(List<Node> freespawnloc, Node playerNode)
    {
        float minDistance = 10f;

        List<Node> distantNodes = freespawnloc.FindAll(n =>
        Vector2.Distance(n.worldPosition, playerNode.worldPosition) >= minDistance);

        spawnedEnemies.Clear();
        enemySpawnPositions.Clear();

        if (Enemy.Count < 3) return;

        int[] enemyCounts = new int[9];

        for (int i = 0; i < 9; i++)
        {
            enemyCounts[i] = Mathf.RoundToInt(room.enemies[i]);
        }


        for (int type = 0; type < 9; type++)
        {
            for (int i = 0; i < enemyCounts[type] && distantNodes.Count > 0; i++)
            {
                int index = Random.Range(0, distantNodes.Count);
                Node node = distantNodes[index];
                distantNodes.RemoveAt(index);
                freespawnloc.Remove(node);

                spawnedEnemyTypes.Add(type);

                enemySpawnPositions.Add(node.worldPosition);
                GameObject enemyPrefab = Enemy[type].prefab;
                GameObject enemy = Instantiate(enemyPrefab, node.worldPosition, Quaternion.identity, transform);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    //so door doesnt spawn in a corner
    Vector2Int GetDoorPosition(int width, int height)
    {
        int edge = Random.Range(0, 4);
        int x = 0, y = 0;

        switch (edge)
        {
            case 0: x = Random.Range(1, width - 1); y = height - 1; break;
            case 1: x = Random.Range(1, width - 1); y = 0; break;
            case 2: x = 0; y = Random.Range(1, height - 1); break;
            case 3: x = width - 1; y = Random.Range(1, height - 1); break;
        }

        return new Vector2Int(x, y);
    }

    //checks if theres no object in the node for pathfinding
    void UpdateWalkableNodes()
    {
        foreach (Node node in grid)
        {
            node.walkable = !Physics2D.OverlapCircle(node.worldPosition, nodeRadius, obstacleMask);
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.Clamp(Mathf.RoundToInt((gridSizeX - 1) * percentX), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt((gridSizeY - 1) * percentY), 0, gridSizeY - 1);

        return grid[x, y];
    }

    //gives the path to enemies
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Vector2Int[] directions = new Vector2Int[]
        {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1)
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

    //private IEnumerator miniMap()
    //{
    //    //waits a frame to get gridsize
    //    yield return null;
    //    GameObject cameraObj = new GameObject("MinimapCamera");
    //    cameraObj.transform.SetParent(transform);

    //    Camera minimapCamera = cameraObj.AddComponent<Camera>();

    //    minimapCamera.orthographic = true;
    //    minimapCamera.orthographicSize = gridWorldSize.y / 2f;

    //   minimapCamera.transform.position = new Vector3(0, 0, -10);

    //    //maybe later
    //    //minimapCamera.cullingMask = LayerMask.GetMask("minimap");

    //    RenderTexture minimapTexture = new RenderTexture((int)gridWorldSize.x * 10, (int)gridWorldSize.y * 10, 16);
    //    minimapTexture.name = "MinimapTexture";
    //    minimapTexture.Create();
    //    minimapCamera.targetTexture = minimapTexture;

    //    if (minimap != null)
    //    {
    //        minimap.texture = minimapTexture;
    //    }
    //}

    //if wanne see the grid
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

    //and respawn call this in player script or wherevever
    public void Respawn()
    {
        room.points = Mathf.Max(0, room.points - 50);//nub;

        if (spawnedPlayer != null)
        {
            spawnedPlayer.transform.position = playerSpawnPosition;
        }

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                spawnedEnemies[i].transform.position = enemySpawnPositions[i];
            }
            else
            {
                int type = spawnedEnemyTypes[i];
                GameObject enemyPrefab = Enemy[type].prefab;
                GameObject enemy = Instantiate(enemyPrefab, enemySpawnPositions[i], Quaternion.identity, transform);
                spawnedEnemies[i] = enemy;
            }
        }
    }
}
