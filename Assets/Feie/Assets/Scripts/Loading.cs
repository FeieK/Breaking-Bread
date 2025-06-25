using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBarFill;
    public TextMeshProUGUI loadingText;
    
    private bool isLoading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadSceneAsync(2));
        StartCoroutine(LoadingTextChange());
    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        isLoading = true;
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        isLoading = false;
    }
    IEnumerator LoadingTextChange()
    {
        while (isLoading)
        {
            loadingText.text = "Loading";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
        yield break;
    }
}
