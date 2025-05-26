using UnityEngine;

public class enemychooser : MonoBehaviour
{
    //to open the anoying ui
    private GameObject enemyui;
    private bool yh;

    private void OnEnable()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            enemyui = canvas.transform.Find("enemyui")?.gameObject;
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
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            yh = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        yh = false;
    }
}
