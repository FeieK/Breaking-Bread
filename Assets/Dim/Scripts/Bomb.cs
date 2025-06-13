using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Explosion;

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(Explosion, this.transform);
        explosion.transform.parent = null;
    }
}
