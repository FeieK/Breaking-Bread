using UnityEngine;

//idk defently not for the door thp
public class door : MonoBehaviour
{
    public roomSelect roomSelect;

    private void OnEnable()
    {
        GameObject script = GameObject.Find("GameController");
        roomSelect = script.GetComponent<roomSelect>();
    }
    void Update()
    {
        //test cheat
        if (Input.GetKeyDown(KeyCode.P))
        {
            room.roomNum++;

            if (room.roomNum == 1)
            {
                roomSelect.room1();
            }

            if (room.roomNum == 2)
            {
                roomSelect.room2();
            }

            if (room.roomNum == 3)
            {
                roomSelect.room3();
            }

            if (room.roomNum == 4)
            {
                roomSelect.room4();
            }

            if (room.roomNum == 5)
            {
                roomSelect.bosroom();
            }

            if (room.roomNum == 6)
            {
                room.roomNum = 0;
                roomSelect.hubworld();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && room.unlock)
        {
            room.roomNum++;

            if (room.roomNum == 1)
            {
                roomSelect.room1();
            }

            if (room.roomNum == 2)
            {
                roomSelect.room2();
            }

            if (room.roomNum == 3)
            {
                roomSelect.room3();
            }

            if (room.roomNum == 4)
            {
                roomSelect.room4();
            }

            if (room.roomNum == 5)
            {
                roomSelect.bosroom();
            }

            if (room.roomNum == 6)
            {
                room.roomNum = 0;
                roomSelect.hubworld();
            }
        }
    }
}
