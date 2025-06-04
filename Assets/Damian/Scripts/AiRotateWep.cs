using UnityEngine;

public class AiRotateWep : MonoBehaviour
{
    private Transform player;
    private void OnEnable()
    {
        player = GameObject.Find("Player(Clone)").transform;

    }
    void Update()
    {
        Vector3 targetPoint = player.position;

        Vector3 rotation = targetPoint - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
