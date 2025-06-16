using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;

public class roomSelect : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;

    //see if its random and keep track of roomnum
    public bool random = true;
    int roomNum = 1;

    //min enemies and the enemy cost
    private int minimumE;
    private int EnemyCost123 = 1;
    private int EnemyCost456 = 2;
    private int EnemyCost789 = 3;    


    //the ui
    public GameObject enemyui;
    public TextMeshProUGUI minEnScore;
    public TextMeshProUGUI totaleEnScore;
    public TextMeshProUGUI roomnum;
    public Button button;

    private int totalCost;

    //dificoulty
    private float diff;


    //the amount of enemys enemies
    public TMP_Dropdown dropdownEnemy1;
    public TMP_Dropdown dropdownEnemy2;
    public TMP_Dropdown dropdownEnemy3;   
    public TMP_Dropdown dropdownEnemy4;
    public TMP_Dropdown dropdownEnemy5;
    public TMP_Dropdown dropdownEnemy6;    
    public TMP_Dropdown dropdownEnemy7;
    public TMP_Dropdown dropdownEnemy8;
    public TMP_Dropdown dropdownEnemy9;


    private int enemy1;
    private int enemy2;
    private int enemy3;    
    private int enemy4;
    private int enemy5;
    private int enemy6;   
    private int enemy7;
    private int enemy8;
    private int enemy9;

    //temporery
    public TextMeshProUGUI lvlshow;
    //!!!!!!!!!!!!!!!!!

    private void Awake()
    {
        //loads the game autosave after 5 min every 5 min
        SaveGame.Load();
        //InvokeRepeating(nameof(SaveGame.Save), 300f, 300f);
        
        //goes to hubworld
        hubworld();
    }
    private void Update()
    {

        //find smt better for this to place add
        lvlshow.text = $" {gameState.level}";
        diff = gameState.GetDifficultyMultiplier();
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!11
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveGame.reset();
        }
        showui();


        //if not all the objects spawn in the hubworld
        if (room.failsave)
        {
            changemap();
        }
    }
    //its a suprise
    public void randomEnemies()
    {
        int enemy1 = 0, enemy2 = 0, enemy3 = 0, enemy4 = 0, enemy5 = 0, enemy6 = 0, enemy7 = 0, enemy8 = 0, enemy9 = 0;
        int totalPoints = 0;

        int level = Mathf.FloorToInt(gameState.level);

        int minEnemy, maxEnemy;

        if (level >= 15)
        {
            //all enemy spawn
            minEnemy = 1;
            maxEnemy = 9;
        }
        else
        {
            //only 3 close to ur lvl
            minEnemy = Mathf.Clamp(level - 1, 1, 9);
            maxEnemy = Mathf.Clamp(minEnemy + 2, 1, 9);
        }

        while (totalPoints < minimumE)
        {
            int choice = Random.Range(minEnemy, maxEnemy + 1);

            switch (choice)
            {
                case 1: enemy1++; totalPoints += EnemyCost123; break;
                case 2: enemy2++; totalPoints += EnemyCost123; break;
                case 3: enemy3++; totalPoints += EnemyCost123; break;
                case 4: enemy4++; totalPoints += EnemyCost456; break;
                case 5: enemy5++; totalPoints += EnemyCost456; break;
                case 6: enemy6++; totalPoints += EnemyCost456; break;
                case 7: enemy7++; totalPoints += EnemyCost789; break;
                case 8: enemy8++; totalPoints += EnemyCost789; break;
                case 9: enemy9++; totalPoints += EnemyCost789; break;
            }
        }

        SetEnemies(enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, enemy7, enemy8, enemy9);
    }

    private void showui()
    {
        switch (roomNum)
        {
            case 1: minimumE = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 2: minimumE = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 3: minimumE = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 4: minimumE = Mathf.RoundToInt(15 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 5: minimumE = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff    ); break;
            default: minimumE = Mathf.RoundToInt(0 * Mathf.Pow(1.1f, gameState.level) * diff); break;
        }


        enemy1 = dropdownEnemy1.value;
        enemy2 = dropdownEnemy2.value;
        enemy3 = dropdownEnemy3.value;
        enemy4 = dropdownEnemy4.value;
        enemy5 = dropdownEnemy5.value;
        enemy6 = dropdownEnemy6.value;
        enemy7 = dropdownEnemy7.value;
        enemy8 = dropdownEnemy8.value;
        enemy9 = dropdownEnemy9.value;

        totalCost = enemy1 * EnemyCost123 + enemy2 * EnemyCost123 + enemy3 * EnemyCost123 + enemy4 * EnemyCost456 + enemy5 * EnemyCost456 + enemy6 * EnemyCost456 + enemy7 * EnemyCost456 + enemy8 * EnemyCost456 + enemy9 * EnemyCost789;

        roomnum.text = $"Room Number: {roomNum}";
        totaleEnScore.text = $"Total Score: {totalCost}";
        minEnScore.text = $"Min Score {minimumE}";
    }

    //needs to choose a enemy for each room or it will be random
    public void choosenemys()
    {

        if (totalCost < minimumE)
        {
            StartCoroutine(FlashUI());
            Debug.Log("Not enough points used! Need at least " + minimumE);
            return;
        }
        else
        {
            if (roomNum == 5)
            {
                enemyui.SetActive(false);
                roomNum = 1;
                random = true;
            }
            else
            {
                roomNum++;
            }

            EnemysEachRoom.AddConfig(enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, enemy7, enemy8, enemy9);
        }
    }

    //sets the enemys to room where it gets ussed to spawn them in grid
    public bool SetEnemies(int enemy1, int enemy2, int enemy3, int enemy4, int enemy5, int enemy6, int enemy7, int enemy8, int enemy9)
    {
        room.enemy1 = enemy1;
        room.enemy2 = enemy2;
        room.enemy3 = enemy3;
        room.enemy4 = enemy4;
        room.enemy5 = enemy5;
        room.enemy6 = enemy6;
        room.enemy7 = enemy7;
        room.enemy8 = enemy8;
        room.enemy9 = enemy9;

        return true;
    }

    //if u dont have enought enemys
    private IEnumerator FlashUI()
    {
        int flashes = 3;
        float flashDuration = 0.2f;

        for (int i = 0; i < flashes; i++)
        {
            SetUIColor(Color.red);
            yield return new WaitForSeconds(flashDuration);

            SetUIColor(Color.white);
            yield return new WaitForSeconds(flashDuration);
        }
        SetUIColor(Color.white);
    }

    private void SetUIColor(Color color)
    {
        minEnScore.color = color;
        totaleEnScore.color = color;
        button.image.color = color;
    }

    //the hubworld
    public void hubworld()
    {
        room.placehubworldobj = true;
        minimumE = 0;

        random = true;

        randomEnemies();

        roomNum = 1;


        room.minsize = 15;
        room.maxsize = 20;

        changemap();
    }


    //5 rooms yes i shoulda done it cleaner ut to late
    public void room1()
    {
        //better to call it where needed but like u dont lose to much
        SaveGame.Save();

        room.placehubworldobj = false;

        minimumE = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff);
        //if u make them random
        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3, config.enemy4, config.enemy5, config.enemy6, config.enemy7, config.enemy8, config.enemy9);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }
        room.minsize = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(25 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff );
        changemap();
    }

    public void room2()
    {

        minimumE = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff);
        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3, config.enemy4, config.enemy5, config.enemy6, config.enemy7, config.enemy8, config.enemy9);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

        room.minsize = Mathf.RoundToInt(25 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(30 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(4 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room3()
    {
        room.placehubworldobj = false;

        minimumE = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff);

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3, config.enemy4, config.enemy5, config.enemy6, config.enemy7, config.enemy8, config.enemy9);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

        room.minsize = Mathf.RoundToInt(30 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(35 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room4()
    {
        room.placehubworldobj = false;

        minimumE = Mathf.RoundToInt(15 * Mathf.Pow(1.1f, gameState.level) * diff);

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3, config.enemy4, config.enemy5, config.enemy6, config.enemy7, config.enemy8, config.enemy9);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

        room.minsize = Mathf.RoundToInt(35 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(6 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }
    public void bosroom()
    {
        room.placehubworldobj = false;

        //or 1 bos
        minimumE = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff);

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3, config.enemy4, config.enemy5, config.enemy6, config.enemy7, config.enemy8, config.enemy9);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }
        room.minsize =  Mathf.RoundToInt(100 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.maxsize = Mathf.RoundToInt(120 * Mathf.Pow(1.1f, gameState.level) * diff  );

        room.placNumObj = Mathf.RoundToInt(12 * Mathf.Pow(1.1f, gameState.level) * diff);
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
