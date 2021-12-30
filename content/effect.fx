#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//float4 colah = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;
	//float4 inverseColor = abs(colah - float4(1.0, 1.0, 1.0, 1.0));
	//float4 outColor = float4(inverseColor.xyz, colah.a);
    //return outColor;
    return (0.0).rrrr;

}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODELMainPS();
	}
};