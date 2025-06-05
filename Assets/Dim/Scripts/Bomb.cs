using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Explosion;

    private void OnDestroy()
    {
        Instantiate(Explosion, this.transform);
    }
}
