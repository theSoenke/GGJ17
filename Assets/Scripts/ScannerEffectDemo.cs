using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
    public bool IsPinging;
	public Material EffectMaterial;
    public Color GlowColor;
    public Transform ScannerOrigin;
    public float ScanDistance;
    public float BorderWidth;
    public float ScanWidth;
    public float MaskOpacity;


    private Camera _camera;


	void OnEnable()
	{
        _camera = GetComponent<Camera>();
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
        EffectMaterial.SetVector("_ScanOrigin", GetScannerSS(ScannerOrigin.position));
        EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
        EffectMaterial.SetFloat("_ScanBorderWidth", BorderWidth);
        EffectMaterial.SetFloat("_ScanWidth", ScanWidth);
        EffectMaterial.SetFloat("_MaskOpacity", MaskOpacity);
        EffectMaterial.SetColor("_Color", GlowColor);
        EffectMaterial.SetInt("_IsPinging", IsPinging ? 1 : 0);
        Apply(src, dst, EffectMaterial);
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
