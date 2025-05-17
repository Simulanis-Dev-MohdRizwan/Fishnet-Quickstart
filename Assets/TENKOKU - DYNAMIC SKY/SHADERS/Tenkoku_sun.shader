Shader "TENKOKU/sun_shader" {

Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_CoronaColor ("Corona Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("BRDF", 2D) = "white" {}
	_overBright ("OverBright", float) = 1.0
	_dispStrength ("Displace Amount", Range(0.0,10.0)) = 1.0
}

	
	SubShader
{
    Tags { "RenderType"="Opaque" }
    Pass
    {
        Name "FORWARD"
        Tags { "LightMode"="ForwardBase" }

        HLSLPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile_fog

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariables.hlsl"

        // Define properties
        float4 _TintColor;
        float4 _CoronaColor;
        float _OverBright;
        float _DispStrength;
        sampler2D _MainTex;
        float4 _MainTex_ST;

        // Vertex shader
        struct Attributes
        {
            float4 position : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 position : SV_POSITION;
            float3 normal : TEXCOORD0;
            float2 uv : TEXCOORD1;
        };

        Varyings vert(Attributes v)
        {
            Varyings o;
            o.position = TransformObjectToHClip(v.position.xyz);
            o.normal = v.normal;
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }

        // Fragment shader
        half4 frag(Varyings i) : SV_Target
        {
            // Sample texture
            half4 texColor = tex2D(_MainTex, i.uv);
            texColor.rgb *= texColor.a;

            // Lighting calculations
            half3 normal = normalize(i.normal);
            half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
            half diff = max(0, dot(normal, lightDir));
            half3 diffuse = diff * texColor.rgb;

            // Apply tint color
            half3 finalColor = diffuse * _TintColor.rgb;

            // Apply overbright effect
            finalColor *= _OverBright;

            return half4(finalColor, texColor.a);
        }
        ENDHLSL
    }
}

}
