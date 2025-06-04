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

            switch (room.roomNum)
            {
                case 1:
                    gameState.AddXP(10);
                    roomSelect.room1();
                    break;
                case 2:
                    gameState.AddXP(20);
                    roomSelect.room2();
                    break;
                case 3:
                    gameState.AddXP(30);
                    roomSelect.room3();
                    break;
                case 4:
                    gameState.AddXP(40);
                    roomSelect.room4();
                    break;
                case 5:
                    gameState.AddXP(45);
                    roomSelect.bosroom();
                    break;
                case 6:
                    gameState.AddXP(55);
                    room.roomNum = 0;
                    roomSelect.hubworld();
                    break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && room.unlock)
        {
            room.roomNum++;

            switch (room.roomNum)
            {
                case 1:
                    gameState.AddXP(10);
                    room.runStartTime = Time.time;
                    roomSelect.room1();
                    break;
                case 2:
                    gameState.AddXP(20);
                    roomSelect.room2();
                    break;
                case 3:
                    gameState.AddXP(30);
                    roomSelect.room3();
                    break;
                case 4:
                    gameState.AddXP(40);
                    roomSelect.room4();
                    break;
                case 5:
                    gameState.AddXP(45);
                    roomSelect.bosroom();
                    break;
                case 6:
                    gameState.AddXP(55);
                    room.roomNum = 0;

                    //calc points
                    float timeTaken = Time.time - room.runStartTime;
                    int timeBonus = Mathf.Max(0, 1000 + (gameState.level * 100) - Mathf.FloorToInt(timeTaken));//the longer it takes the more points 1000 max with scaling on xp for higher lvl

                    room.points += timeBonus;
                    Debug.Log($"Run completed in {timeTaken:F1} seconds. Points awarded: {timeBonus}   whoohooo so many points u pro (incase u have low bcs not gonne make a check) hahahah nub");
                    roomSelect.hubworld();
                    break;
            }
        }
    }
}
