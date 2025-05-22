using UnityEngine;

[System.Serializable]
//so game knows what size the object is for spawning
public class obj
{
    public GameObject prefab;
    public Vector2Int size = Vector2Int.one; 
    public bool canRotate = false;
}

