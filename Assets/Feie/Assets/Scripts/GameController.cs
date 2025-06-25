using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public bool roundIsActive;
    public bool paused;
    public int playerLives;
    [HideInInspector] public int sceneIndex;

    private CanvasGroup canvasGroup;
    private GameObject player;
    private PlayerSystem playerScript;
    private GameObject spawn;   // The spawn variable

    [SerializeField] private GameObject[] scoreObj;
    [SerializeField] private GameObject darkenScreenObj;
    private Image darkenScreenImage;
    private Score scoreScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GameObject.Find("Death Message Group").GetComponent<CanvasGroup>();

        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Respawn");    // Setting the spawn variable

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 2)
        {
            playerScript = player.GetComponent<PlayerSystem>();
            scoreScript = scoreObj[1].GetComponent<Score>();
            darkenScreenImage = darkenScreenObj.GetComponent<Image>();
        }

    }

    public void DEBUGDIE()
    {
        playerLives = 1;
        gameState.pHp = 0;
    }
    public void Die()
    {
        roundIsActive = false;
        if (playerLives > 1)
        {
            playerLives -= 1;
            StartCoroutine(YouDied());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator YouDied()
    {
        bool transition = true;
        darkenScreenObj.SetActive(true);
        while (transition)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha = canvasGroup.alpha + 1.5f * Time.deltaTime;
                yield return null;
            }
            else
            {
                transition = false;
            }
        }
        yield return new WaitForSeconds(1);
        transition = true;
        while (transition)
        {
            if (darkenScreenImage.color.a < 1)
            {
                darkenScreenImage.color = new Color(darkenScreenImage.color.r, darkenScreenImage.color.g, darkenScreenImage.color.b, darkenScreenImage.color.a + 1f * Time.deltaTime);
                yield return null;
            }
            else
            {
                transition = false;
            }
        }
        yield return new WaitForSeconds(2);
        player.transform.position = spawn.transform.position;   // Setting the player position to the spawn position (Remove when merging)
        canvasGroup.alpha = 0;
        transition = true;
        while (transition)
        {
            if (darkenScreenImage.color.a > 0)
            {
                darkenScreenImage.color = new Color(darkenScreenImage.color.r, darkenScreenImage.color.g, darkenScreenImage.color.b, darkenScreenImage.color.a - 1.5f * Time.deltaTime);
                yield return null;
            }
            else
            {
                transition = false;
            }
        }
        darkenScreenObj.SetActive(false);
        roundIsActive = true;
        yield break;
    }
    IEnumerator GameOver()
    {
        int transition = 0;
        darkenScreenObj.SetActive(true);
        while (transition == 0)
        {
            if (darkenScreenImage.color.a < 1)
            {
                darkenScreenImage.color = new Color(darkenScreenImage.color.r, darkenScreenImage.color.g, darkenScreenImage.color.b, darkenScreenImage.color.a + 1f * Time.deltaTime);
                yield return null;
            }
            else
            {
                transition = 1;
            }
        }
        scoreObj[0].SetActive(true);
        yield return new WaitForSeconds(1);
        while (transition == 1)
        {
            if (darkenScreenImage.color.a > 0)
            {
                darkenScreenImage.color = new Color(darkenScreenImage.color.r, darkenScreenImage.color.g, darkenScreenImage.color.b, darkenScreenImage.color.a - 1.5f * Time.deltaTime);
                yield return null;
            }
            else
            {
                transition = 2;
            }
        }
        yield return new WaitForSeconds(0.5f);
        darkenScreenObj.SetActive(false);
        scoreScript.FunctionUpdateScore();
        yield break;
    }

    public void Timescale()
    {
        Time.timeScale = 1.0f;
    }

}
