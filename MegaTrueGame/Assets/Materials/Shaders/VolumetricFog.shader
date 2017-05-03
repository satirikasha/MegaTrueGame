Shader "Enviroment/VolumetricFog"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD0;
				float4 projPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D_float _CameraDepthTexture;
			half4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex1 = tex2D(_MainTex, i.worldPos.xz / 7 + _Time.y / 15);
				fixed4 tex2 = tex2D(_MainTex, i.worldPos.xz / 3 + (_Time.y / 15) * half2(1,-0.5));
				fixed4 tex = (tex1 + tex2) / 2;
				//tex.b = 1 - (tex.r + tex.g);
				fixed3 normal = normalize(tex.rbg * 2 - 1);

				float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
				float partZ = i.projPos.z;
				float fade = saturate(0.5 * (sceneZ - partZ)) * _Color.a;

				fixed4 color = UNITY_LIGHTMODEL_AMBIENT.rgba;

				color.a = fade + fade * tex.a;

				if (0.0 == _WorldSpaceLightPos0.w) // directional light?
				{
					half3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					color.rgb *= (1 + dot(normal, lightDirection));
				}

				return color;
			}
			ENDCG
		}
	}
}
