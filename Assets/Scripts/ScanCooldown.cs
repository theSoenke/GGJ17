using UnityEngine;
using UnityEngine.UI;

public class ScanCooldown : MonoBehaviour
{
    private Slider slider;


    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float scanCooldown = GameController.Instance.ScanCooldown;
        print(scanCooldown);
        slider.value = scanCooldown;
    }
}
