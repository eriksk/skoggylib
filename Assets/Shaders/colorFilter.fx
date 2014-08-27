sampler2D tex : register(S0);
sampler2D refractSampler;

float burn = 0.01;
float saturation = 0.05;
float r = 1.0;
float g = 1.0;
float b = 1.0;
float brightness = 0.0;
float refractionMagnitude = 1.0;

float2 GetDiff(float2 _tex)
{
	float2 diff;
	float2 tex = _tex;
		float2 btex = _tex;
		tex.x -= 0.003;
	btex.x += 0.003f;
	diff.x = tex2D(refractSampler, tex).r - tex2D(refractSampler, btex).r;
	btex = _tex;
	tex.y -= 0.003;
	btex.y += 0.003;
	diff.y = tex2D(refractSampler, tex).r - tex2D(refractSampler, btex).r;
	tex = _tex;
	diff *= (refractionMagnitude * tex2D(refractSampler, tex).r);
	return diff;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float2 t = uv + GetDiff(uv) * 0.1f;
	float4 col = tex2D(tex, t);

	float d = sqrt(pow(uv.x - 0.5, 2) + pow(uv.y - 0.5, 2));

	col.rgb -= d * burn;
	float a = col.r + col.g + col.b;
	a /= 3.0;
	a *= 1.0 - saturation;
	col.r = (col.r * saturation + a) * r;
	col.g = (col.g * saturation + a) * g;
	col.b = (col.b * saturation + a) * b;
	col.rgb += brightness;

	return col;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}