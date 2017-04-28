Shader "Enviroment/Terrain" {
	Properties {
		_SplatMap ("Splat Map", 2D) = "white" {}
		_MacroVariation("Macro Variation", 2D) = "white" {}
		_MacroScale("Macro Variation Scale", Range(0.1, 250)) = 1
		_Ground("Ground", 2D) = "Brown" {}
		_GroundScale("Ground Scale", Range(0.1, 10)) = 1
	    _GroundSharpness("Ground Sharpness", Range(0, 0.9)) = 0.8
		_Grass("Grass", 2D) = "Green" {}
		_GrassScale("Grass Scale", Range(0.1, 10)) = 1
		_GrassSharpness("Grass Sharpness", Range(0, 0.9)) = 0.8
		_Rock("Rock", 2D) = "Gray" {}
		_RockScale("Rock Scale", Range(0.1, 10)) = 1
		_RockSharpness("Rock Sharpness", Range(0, 0.9)) = 0.8
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf StandardSpecular fullforwardshadows
		#pragma target 3.0
		#include "Blend.cginc"

		struct Input {
			float2 uv_SplatMap;
			float3 worldPos;
		};

		sampler2D _SplatMap;
		sampler2D _MacroVariation;
		sampler2D _Ground;
		sampler2D _Grass;
		sampler2D _Rock;
		half _MacroScale;
		half _GroundScale;
		half _GrassScale;
		half _RockScale;
		half _GroundSharpness;
		half _GrassSharpness;
		half _RockSharpness;
		half _Glossiness;
		half _Metallic;


		fixed3 BlendSplatMap(fixed3 splatMap, fixed3 detailMap) {
			return saturate(max(splatMap * detailMap * 5, splatMap));// saturate(max(splatMap, lerp(splatMap, splatMap * detailMap * 4, 0.25)));
		}

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			fixed4 splatMap = tex2D(_SplatMap, IN.uv_SplatMap);
			fixed4 macroVar = PlanarMap(_MacroVariation, IN.worldPos, _MacroScale);
			splatMap.rgb = BlendSplatMap(splatMap.rgb, macroVar.rgb);
			//splatMap.g = saturate(splatMap.g + 1 - dot(float3(0, 1, 0), o.Normal));

			fixed4 ground = PlanarMap(_Ground, IN.worldPos, _GroundScale);
			fixed4 grass = PlanarMap(_Grass, IN.worldPos, _GrassScale);
			fixed4 rock = TriplanarMap(_Rock, IN.worldPos, o.Normal, _RockScale);


			fixed4 result = ground;

			//saturate(splatMap.g + (1 - splatMap.g) * (rock.a * 2 - 1))
			//saturate(splatMap.g - (1 - rock.a) * (1 - splatMap.g))

			/*result = lerp(result, grass, saturate(splatMap.r - (1 - grass.a) * (1 - splatMap.r) * _GrassSharpness));
			result = lerp(result, rock, saturate(splatMap.g - (1 - rock.a) * (1 - splatMap.g) * _RockSharpness));*/

			result = BlendMap(grass, result, splatMap.r, _GrassSharpness);
			result = BlendMap(rock, result, splatMap.g, _RockSharpness);

			//o.Albedo = splatMap.r;
			o.Albedo = result.rgb * (macroVar.a / 3 + 0.65);
			o.Occlusion = result.a / 2 + 0.5;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
