Shader "Enviroment/Standard" {
	Properties {
		_MainTex ("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Specular("Specular", Range(0,1)) = 0.0
		_Cover("Cover", 2D) = "black" {}
		_CoverScale("Cover Scale", Range(0.1, 10)) = 1
		_CoverSharpness("Cover Sharpness", Range(0.1, 0.9)) = 0.8
		_CoverAmmount("Cover Ammount", Range(0,1)) = 0.5
		_CoverGlossiness("Smoothness", Range(0,1)) = 0.5
		_CoverSpecular("Specular", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf StandardSpecular fullforwardshadows
		#pragma target 3.0
		#include "Blend.cginc"

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal; INTERNAL_DATA
		};

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _Cover;
		half _Glossiness;
		half _Specular;
		half _CoverScale;
		half _CoverSharpness;
		half _CoverAmmount;
		half _CoverGlossiness;
		half _CoverSpecular;
		

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {			
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 color = fixed4(tex.rgb, 1 - _CoverSharpness);
			fixed4 cover = PlanarMap(_Cover, IN.worldPos, _CoverScale);
			fixed3 normal = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));
			fixed blend = saturate(dot(half3(0, 1, 0), WorldNormalVector(IN, normal)) - (tex.a * 2 - 1) * 0.5 + (_CoverAmmount * 2 - 1));
			fixed factor = GetBlendFactor(cover, color, blend, _CoverSharpness);

			o.Albedo = lerp(cover, color, factor);
			o.Normal = normal;
			o.Specular = lerp(_CoverSpecular, _Specular, factor);
			o.Smoothness = lerp(_CoverGlossiness, _Glossiness, factor);
			o.Occlusion = tex.a * lerp(cover.a, 1, factor);
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
