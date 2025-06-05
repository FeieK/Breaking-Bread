using UnityEngine;

//idk defently not for the door thp
public class door : MonoBehaviour
{
    public roomSelect roomSelect;

    private float diff;

    private void OnEnable()
    {
        GameObject script = GameObject.Find("GameController");
        roomSelect = script.GetComponent<roomSelect>();
        diff = gameState.GetDifficultyMultiplier();

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
                    gameState.AddXP(Mathf.RoundToInt(10 * diff));
                    roomSelect.room1();
                    break;
                case 2:
                    gameState.AddXP(Mathf.RoundToInt(20 * diff));
                    roomSelect.room2();
                    break;
                case 3:
                    gameState.AddXP(Mathf.RoundToInt(30 * diff));
                    roomSelect.room3();
                    break;
                case 4:
                    gameState.AddXP(Mathf.RoundToInt(40 * diff));
                    roomSelect.room4();
                    break;
                case 5:
                    gameState.AddXP(Mathf.RoundToInt(45 * diff));
                    roomSelect.bosroom();
                    break;
                case 6:
                    gameState.AddXP(Mathf.RoundToInt(55 * diff));
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
                    room.runStartTime = Time.time;
                    roomSelect.room1();
                    break;
                case 2:
                    gameState.AddXP(Mathf.RoundToInt(20 * diff));
                    gameState.gold += (Mathf.RoundToInt(10 * diff));
                    roomSelect.room2();
                    break;
                case 3:
                    gameState.AddXP(Mathf.RoundToInt(30 * diff));
                    gameState.gold += (Mathf.RoundToInt(10 * diff));
                    roomSelect.room3();
                    break;
                case 4:
                    gameState.AddXP(Mathf.RoundToInt(40 * diff));
                    gameState.gold += (Mathf.RoundToInt(25 * diff));
                    roomSelect.room4();
                    break;
                case 5:
                    gameState.AddXP(Mathf.RoundToInt(50 * diff));
                    gameState.gold += (Mathf.RoundToInt(25 * diff));
                    roomSelect.bosroom();
                    break;
                case 6:
                    gameState.AddXP(Mathf.RoundToInt(60 * diff));
                    gameState.gold += (Mathf.RoundToInt(30 * diff));
                    room.roomNum = 0;

                    //calc points
                    float timeTaken = Time.time - room.runStartTime;
                    int timeBonus = Mathf.Max(0, 1000 + (gameState.level * 100) - Mathf.FloorToInt(timeTaken));//the longer it takes the more points 1000 max with scaling on xp for higher lvl

                    room.points += timeBonus;
                    Debug.Log($"Run completed in {timeTaken:F1} seconds. Points awarded: {timeBonus}   whoohooo so many points u pro (incase u have low ponts) hahahah nub");
                    roomSelect.hubworld();
                    break;
            }
        }
    }
}
