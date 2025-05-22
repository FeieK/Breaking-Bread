using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    void Update()
    {
        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = targetPoint - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
