Shader "Special/ScannerEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
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
			float4 _ScanOrigin;
			float _ScanDistance;
			float _ScanWidth;
			float _ScanOpacity;
			int _IsPinging;
			float4 _Color;

			
			half4 frag (VertOut i) : SV_Target
			{
				half4 texCol = tex2D(_MainTex, i.uv);
				float dist = distance(_ScanOrigin, i.vertex);
				half4 finalCol = texCol;

				if (_IsPinging != 0)
				{
					if (dist > (_ScanDistance - _ScanWidth) && dist < _ScanDistance)
					{
						finalCol = lerp(texCol, _Color, _ScanOpacity);
					}
				}
				

				return finalCol;
			}
			ENDCG
		}
	}
}
