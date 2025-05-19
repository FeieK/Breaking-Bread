using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool roundIsActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        roundIsActive = false;
    }
    public void Die()
    {
        roundIsActive = false;
    }
}
