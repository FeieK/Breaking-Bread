using UnityEngine;

public class GameOverCollision : MonoBehaviour
{
    private GameOverAnimation parentScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentScript = GetComponentInParent<GameOverAnimation>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        parentScript.PlaySound();
    }
}
