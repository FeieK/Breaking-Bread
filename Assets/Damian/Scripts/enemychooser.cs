using UnityEngine;

public class enemychooser : MonoBehaviour
{
    //to open the anoying ui
    private GameObject enemyui;
    private bool yh;

    private void OnEnable()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform shops = canvas.transform.GetChild(0);
        if (canvas != null)
        {
            enemyui = shops.Find("enemyui")?.gameObject;
            if (enemyui == null)
                Debug.LogWarning("enemyui not found inside Canvas!");
        }
        else
        {
            Debug.LogWarning("Canvas not found in scene!");
        }
    }
    private void Update()
    {

        if (yh && Input.GetKeyDown(KeyCode.E))
        {
            enemyui.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            yh = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        yh = false;
    }
}
