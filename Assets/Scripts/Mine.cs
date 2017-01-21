using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionEffect;


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // BOOM
        }
    }
}
