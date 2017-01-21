using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionPrefab;


    private void OnTriggerEnter(Collider col)
    {
        print("BOOM");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 5);
        GameController.Instance.HitMine();
    }
}
