using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Image img;

    void Update()
    {
        img.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
