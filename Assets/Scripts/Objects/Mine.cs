using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float blinkTime = 1;

    private Renderer mineRenderer;


    private void Awake()
    {
        mineRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider col)
    {
        print("BOOM");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 5);
        GameController.Instance.HitMine();
    }

    public void ShowMine()
    {
        StartCoroutine(Blink(blinkTime));
    }

    private IEnumerator Blink(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            mineRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            mineRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
