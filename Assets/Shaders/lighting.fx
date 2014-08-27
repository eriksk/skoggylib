sampler2D tex : register(S0);
sampler2D lightmap;

float4 ambientColor;
float globalLightIntensity = 1.0;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 diffuse = tex2D(tex, uv);
	float4 light = tex2D(lightmap, uv);

	float3 ambient = ambientColor.rgb * ambientColor.a;

	float3 intensity = ambient + light.rgb + (light * globalLightIntensity);

	float3 final = diffuse.rgb * intensity;

	return float4(final, diffuse.a);
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}