using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManeger : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;

    private void Start()
    {
        //to see map generations
       // InvokeRepeating(nameof(changemap), 0f, 2f);
    }
    void Update()
    {
        //testing
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            changemap();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            grid.Respawn();
        }
    }

    //gues
    public void changemap()
    {
        StartCoroutine(ReloadMap());
    }

    private IEnumerator ReloadMap()
    {
        map.SetActive(false);
        //waits a frame to not break uhm everything
        yield return null;
        map.SetActive(true);
    }

}
