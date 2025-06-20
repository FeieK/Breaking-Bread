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
    int roomNum = 1;

    //min enemies and the enemy cost
    private int minimumE;
    private int[] enemyCosts = new int[9] { 1, 1, 1, 2, 2, 2, 3, 3, 3 };



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
    public TMP_Dropdown[] dropdownEnemies = new TMP_Dropdown[9];

    private int[] enemies = new int[9];

    private bool random = true;
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


        int[] enemies = new int[9];

        while (totalPoints < minimumE)
        {
            int choice = Random.Range(minEnemy - 1, maxEnemy); // choice is now index 0–8
            enemies[choice]++;
            totalPoints += enemyCosts[choice];
        }

        SetEnemies(enemies);
        EnemysEachRoom.AddConfig(enemies);
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

        int[] enemies = new int[9];
        totalCost = 0;

        for (int i = 0; i < 9; i++)
        {
            enemies[i] = dropdownEnemies[i].value;
            totalCost += enemies[i] * enemyCosts[i];
        }


        roomnum.text = $"Order: {roomNum}";
        totaleEnScore.text = $"Total Points: {totalCost}";
        minEnScore.text = $"Minimum Point Needed: {minimumE}";
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
            }
            else
            {
                roomNum++;
            }
            random = false;
            EnemysEachRoom.AddConfig(enemies);
        }
    }

    //sets the enemys to room where it gets ussed to spawn them in grid
    public bool SetEnemies(int[] enemies)
    {
        for (int i = 0; i < 9; i++)
        {
            room.enemies[i] = enemies[i];
        }
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

        //cleanest way to fix
        if (random)
        {
            EnemysEachRoom.roomConfigs.RemoveAt(0);
        }

        if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); //random enemys
            }

        room.minsize = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(25 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(6 * Mathf.Pow(1.1f, gameState.level) * diff );
        changemap();
    }

    public void room2()
    {

            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // random enemys
            }
        

        room.minsize = Mathf.RoundToInt(25 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(30 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(8 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room3()
    {
        minimumE = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff);


            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // randome enemys
            }

        room.minsize = Mathf.RoundToInt(30 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(35 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room4()
    {
        minimumE = Mathf.RoundToInt(15 * Mathf.Pow(1.1f, gameState.level) * diff);

            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // random enemys
            }

        room.minsize = Mathf.RoundToInt(35 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(12 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }
    public void bosroom()
    {
        //or 1 bos
        minimumE = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff);


            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // random enemys
            }
        
        room.minsize =  Mathf.RoundToInt(100 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.maxsize = Mathf.RoundToInt(120 * Mathf.Pow(1.1f, gameState.level) * diff  );

        room.placNumObj = Mathf.RoundToInt(24 * Mathf.Pow(1.1f, gameState.level) * diff);
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
