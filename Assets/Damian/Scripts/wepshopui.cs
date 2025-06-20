using UnityEngine;

public class wepshopui : MonoBehaviour
{
    //to open the anoying ui
    private GameObject wepui;
    private bool yh;

    private void OnEnable()
    {
        //u in the triger earia u can pres e to oppen ui
        GameObject canvas = GameObject.Find("Canvas");
        Transform shops = canvas.transform.GetChild(0);
        if (canvas != null)
        {
            wepui = shops.Find("wepShop")?.gameObject;
            if (wepui == null)
                Debug.LogWarning("wepui not found inside Canvas!");
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
            wepui.SetActive(true);
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