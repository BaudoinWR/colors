Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorToChange("Color You Want To Change", Color) = (0,0,1,1)
        _DesiredColor("Desired Color ", Color) = (1,0,0,1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			float4 _ColorToChange;
			float4 _DesiredColor;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

		    struct Fragment
			{
				float4 vertex : POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				col = 1 - col;
				return col;
			}

			float4 frag(Fragment IN) : COLOR
			{
				half4 c = tex2D(_MainTex, IN.uv_MainTex);

					if (c.r >= _ColorToChange.r - 0.005 && c.r <= _ColorToChange.r + 0.005
					&& c.g >= _ColorToChange.g - 0.005 && c.g <= _ColorToChange.g + 0.005
						&& c.b >= _ColorToChange.b - 0.005 && c.b <= _ColorToChange.b + 0.005)
				{
					return _DesiredColor;
				}

				return c;
			}

			ENDCG
		}
	}
}
