using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class roomSelect : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;

    private void Start()
    {
        hubworld();
    }
    private void Update()
    {
        if (room.failsave)
        {
            changemap();
        }
    }
    public void hubworld()
    {
        room.placehubworldobj = true;
        room.enemy1 = 0;
        room.enemy2 = 0;
        room.enemy3 = 0;

        room.minsize = 30;
        room.maxsize = 35;

        changemap();
    }

    public void room1()
    {
        room.placehubworldobj = false;

        room.enemy1 = 3;
        room.enemy2 = 0;
        room.enemy3 = 0;

        room.minsize = 35;
        room.maxsize = 40;

        room.placNumObj = 3;
        changemap();
    }

    public void room2()
    {
        room.placehubworldobj = false;

        room.enemy1 = 2;
        room.enemy2 = 2;
        room.enemy3 = 0;

        room.minsize = 40;
        room.maxsize = 45;

        room.placNumObj = 4;
        changemap();
    }

    public void room3()
    {
        room.placehubworldobj = false;

        room.enemy1 = 1;
        room.enemy2 = 2;
        room.enemy3 = 2;

        room.minsize = 45;
        room.maxsize = 50;

        room.placNumObj = 5;
        changemap();
    }

    public void room4()
    {
        room.placehubworldobj = false;

        room.enemy1 = 0;
        room.enemy2 = 1;
        room.enemy3 = 4;

        room.minsize = 50;
        room.maxsize = 60;

        room.placNumObj = 6;
        changemap();
    }
    public void bosroom()
    {
        room.placehubworldobj = false;

        //or 1 bos
        room.enemy1 = 5;
        room.enemy2 = 10;
        room.enemy3 = 20;

        room.minsize = 100;
        room.maxsize = 120;

        room.placNumObj = 12;
        changemap();
    }

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
