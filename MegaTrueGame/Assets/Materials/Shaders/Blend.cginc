fixed4 PlanarMap(sampler2D tex, float3 pos, half scale) {
	return tex2D(tex, pos.xz / scale).rgba;
}

fixed4 TriplanarMap(sampler2D tex, float3 pos, float3 normal, half scale) {
	half3 blend = pow(abs(normal), 10);
	blend /= dot(blend, 1.0);

	fixed4 cx = tex2D(tex, pos.yz / scale);
	fixed4 cy = tex2D(tex, pos.xz / scale);
	fixed4 cz = tex2D(tex, pos.xy / scale);

	return cx * blend.x + cy * blend.y + cz * blend.z;
}

fixed GetBlendFactor(fixed tex1, fixed tex2, fixed blend, fixed sharpness) {
	tex1 *= sharpness;
	tex2 *= sharpness;
	float ma = max(tex1 + blend, tex2 + (1 - blend)) - (1 - sharpness);

	float b1 = max(tex1 + blend - ma, 0);
	float b2 = max(tex2 + (1 - blend) - ma, 0);

	return b2 / (b1 + b2);
}

fixed4 BlendMap(fixed4 tex1, fixed4 tex2, fixed blend, fixed sharpness) {
	return lerp(tex1, tex2, GetBlendFactor(tex1.a, tex2.a, blend, sharpness));
}
