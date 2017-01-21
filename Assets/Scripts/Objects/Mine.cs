using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionEffect;


    private void OnTriggerEnter(Collider col)
    {
        // BOOM
        print("Boom");
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameController.Instance.HitMine();
    }
}
