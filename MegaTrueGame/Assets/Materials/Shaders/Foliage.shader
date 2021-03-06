﻿Shader "Enviroment/Foliage" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal("Normal", 2D) = "blue" {}
	    _Ammount("Ammount", Range(0, 1)) = 0
		_AlphaClip("Alpha Clip", Range(0, 1)) = 0.1
		_Glossiness("Smoothness", Range(0, 1)) = 0.5
		_Specular("Specular", Range(0, 1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular addshadow fullforwardshadows vertex:vert
		#pragma target 3.0
		#pragma glsl

		struct Input {
			float2 uv_MainTex;
			float3 color : COLOR;
		};

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D_half _WindOffsetTex;
		half3 _WindDir;
		half _WindScale;
		half _WindSpeed;
		half _Glossiness;
		half _Specular;
		half _Ammount;
		half _AlphaClip;

		void WindOffset(inout appdata_full v) {
			half3 worldPos = mul(unity_ObjectToWorld, v.vertex.xyz);
			half windOffset = tex2Dlod(_WindOffsetTex, half4(worldPos.xz / _WindScale - _Time.y * _WindDir.xz * _WindSpeed, 0, 0));
			worldPos += _WindDir * (1 - v.color.r) * windOffset * _Ammount;
			v.vertex.xyz = mul(unity_WorldToObject, worldPos);
		}

		void vert(inout appdata_full v) {
			WindOffset(v);
		}

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			clip(c.a - _AlphaClip);
			fixed4 normal = tex2D(_Normal, IN.uv_MainTex);

			o.Albedo = c.rgb;
			o.Normal = normal.rgb * 2 - 1;
			o.Emission = c.rgb * normal.a;
			o.Specular = _Specular;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
