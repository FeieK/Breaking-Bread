using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI killsText;
    public int killsScore;
    [SerializeField] private TextMeshProUGUI timeText;
    public int timeScore;
    [SerializeField] private TextMeshProUGUI damageText;
    public int damageScore;
    [SerializeField] private TextMeshProUGUI totalText;
    private int totalScore;

    private int[] speed = { 1, 1, 1, 1 };

    public void FunctionUpdateScore()
    {
        totalScore = killsScore + timeScore - damageScore;
        StartCoroutine(UpdateScore());
    }
    private IEnumerator UpdateScore()
    {
        int currentKillScore = 0;
        int currentTimeScore = 0;
        int currentDamageScore = 0;
        int currentTotalScore = 0;
        int[] scoreArray = { killsScore, timeScore, damageScore, totalScore };
        for (int i = 0; i < 4; i++)
        {
            if (scoreArray[i] > 10000)
            {
                speed[i] = 20;
            }
            else if (scoreArray[i] > 1000)
            {
                speed[i] = 10;
            }
        }

        while (currentKillScore < killsScore)
        {
            currentKillScore = (Random.Range(1, 5) * speed[0]) + currentKillScore;
            if (currentKillScore > killsScore)
            {
                currentKillScore = killsScore;
            }
            if (currentKillScore <= 9999)
            {
                killsText.text = currentKillScore.ToString();
            }
            else
            {
                killsText.text = (currentKillScore / 1000).ToString().Split(".")[0] + "K";
            }
            yield return new WaitForEndOfFrame();
        }
        while (currentTimeScore < timeScore)
        {
            currentTimeScore = (Random.Range(1, 5) * speed[1]) + currentTimeScore;
            if (currentTimeScore > timeScore)
            {
                currentTimeScore = timeScore;
            }
            if (currentTimeScore <= 9999)
            {
                timeText.text = currentTimeScore.ToString();
            }
            else
            {
                timeText.text = (currentTimeScore / 1000).ToString().Split(".")[0] + "K";
            }
            yield return new WaitForEndOfFrame();
        }
        while (currentDamageScore < damageScore)
        {
            currentDamageScore = (Random.Range(1, 5) * speed[2]) + currentDamageScore;
            if (currentDamageScore > damageScore)
            {
                currentDamageScore = damageScore;
            }
            if (currentDamageScore <= 9999)
            {
                damageText.text = "-" + currentDamageScore.ToString();
            }
            else
            {
                damageText.text = "-" + (currentDamageScore / 1000).ToString().Split(".")[0] + "K";
            }
            yield return new WaitForEndOfFrame();
        }
        while (currentTotalScore < totalScore)
        {
            currentTotalScore = (Random.Range(1, 5) * speed[3]) + currentTotalScore;
            if (currentTotalScore > totalScore)
            {
                currentTotalScore = totalScore;
            }
            if (currentTotalScore <= 9999)
            {
                totalText.text = currentTotalScore.ToString();
            }
            else
            {
                totalText.text = (currentTotalScore / 1000).ToString().Split(".")[0] + "K";
            }
            yield return new WaitForSeconds(0.01f / speed[3]);
        }
        yield break;
    }
}
