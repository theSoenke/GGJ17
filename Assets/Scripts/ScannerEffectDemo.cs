using UnityEngine;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
    public bool IsPinging;
    public Material EffectMaterial;
    public Color GlowColor;
    public Transform ScannerOrigin;
    public float ScanDistance;
    public float ScanWidth;
    [Range(0, 1)]
    public float ScanOpacity;


    private Camera _camera;
    private Animator _animator;


    void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _animator = GetComponent<Animator>();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        EffectMaterial.SetVector("_ScanOrigin", GetScannerSS(ScannerOrigin.position));
        EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
        EffectMaterial.SetFloat("_ScanWidth", ScanWidth);
        EffectMaterial.SetFloat("_ScanOpacity", ScanOpacity);
        EffectMaterial.SetColor("_Color", GlowColor);
        EffectMaterial.SetInt("_IsPinging", IsPinging ? 1 : 0);
        Apply(src, dst, EffectMaterial);
    }

    public void RunScan()
    {
        _animator.Play("SonarAnimation");
    }

    Vector2 GetScannerSS(Vector3 ScannerWS)
    {
        var res = _camera.WorldToScreenPoint(ScannerWS);
        var invertedVertical = new Vector3(res.x, Screen.height - res.y, res.z);
        return invertedVertical;
    }

    void Apply(RenderTexture source, RenderTexture dest, Material mat)
    {
        RenderTexture.active = dest;
        mat.SetTexture("_MainTex", source);
        Graphics.Blit(source, dest, mat);
    }
}
