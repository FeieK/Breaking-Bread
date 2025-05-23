using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public bool roundIsActive;
    public int playerLives;

    private GameOverAnimation gameOverAnimation;
    private CanvasGroup canvasGroup;
    private Image darkenScreenImage;
    private GameObject player;
    private GameObject spawn;   // The spawn variable


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverAnimation = GameObject.Find("Game Over (FakeUI)").GetComponent<GameOverAnimation>();
        canvasGroup = GameObject.Find("Death Message Group").GetComponent<CanvasGroup>();
        darkenScreenImage = GameObject.Find("DarkenScreen").GetComponent<Image>();

        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Respawn");    // Setting the spawn variable
    }

    public void PauseGame()
    {
        roundIsActive = false;
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
        Debug.Log("Darken: Done");
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
        roundIsActive = true;
        yield break;
    }
    IEnumerator GameOver()
    {
        foreach (SpriteRenderer sr in gameOverAnimation.srs)
        {
            sr.enabled = true;
        }
        gameOverAnimation.gameOver = true;
        yield return new WaitForSeconds(7);
        gameOverAnimation.stage = 1;
        yield return new WaitForSeconds(2.2f);
        gameOverAnimation.stage = 2;
        // yield return new WaitForSeconds(x); make sure to put in the right value in x
        // Make it switch to main menu
        yield break;
    }


}
