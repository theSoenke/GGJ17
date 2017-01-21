Shader "Special/ScannerEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DetailTex("Texture", 2D) = "white" {}		
		_ScanDistance("Scan Distance", float) = 0
		_ScanWidth("Scan Width", float) = 10
		_Color("Color", Color) = (1, 1, 1, 0)
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct VertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct VertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 _MainTex_TexelSize;
			float4 _CameraWS;

			VertOut vert(VertIn v)
			{
				VertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv.xy;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _DetailTex;
			float4 _ScanOrigin;
			float _ScanDistance;
			float _ScanWidth;
			float _ScanBorderWidth;
			float _MaskOpacity;
			int _IsPinging;
			
			float4 _Color;

			
			half4 frag (VertOut i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.uv);
				float dist = distance(_ScanOrigin, i.vertex);
				float mask = _MaskOpacity;
				half4 glow = half4(0,0,0,0);

				if (_IsPinging == 1)
				{
					if (dist < _ScanDistance - (_ScanWidth + _ScanBorderWidth))
					{
						mask = clamp(1 - _ScanBorderWidth / clamp(dist - (_ScanDistance - _ScanWidth + _ScanBorderWidth), 0.01f, 1),
							0, _MaskOpacity);
					}
					else if (dist > _ScanDistance - _ScanBorderWidth)
					{
						mask = clamp(_ScanBorderWidth / (dist - (_ScanDistance - _ScanBorderWidth)),
							0, _MaskOpacity);
						if (dist < _ScanDistance)
							glow = half4(_Color.r, _Color.g, _Color.b, mask);
					}
				}
				else if (dist > 40)
				{
					mask = clamp(10 / (dist - 40),
						0, _MaskOpacity);
				}
				
				

				return col * mask + glow;
			}
			ENDCG
		}
	}
}
