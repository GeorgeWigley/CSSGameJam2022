Shader "Custom/MatrixEffect"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Scroll ("Scroll", Vector) = (1,0,0,0)
        _SplitOffset ("Split", Range(0,1)) = 0.2
        _Offset("Offset", Float) = 0.3
        [HDR]_GridColor ("GridColor", Color) = (1,1,1,1)
        [HDR]_TextureColor ("TextureColor", Color) = (1,1,1,1)
        _GridSize ("Grid Scale", Float) = 100
        _GridWidth ("Grid Width", Range(0,1)) = 0.1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half4 _GridColor;
        half4 _TextureColor;

        fixed _SplitOffset;
        fixed _Offset;

        fixed _GridWidth;
        half _GridSize;

        half2 _Scroll;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed grid = step(_GridWidth, ((IN.uv_MainTex.x * _GridSize) % 1)) * step(_GridWidth, ((IN.uv_MainTex.y * _GridSize) % 1));

            half split = ceil((IN.uv_MainTex.x % 1) / _SplitOffset);
            half2 uv = IN.uv_MainTex + _Scroll * _Time.y * sin(split) * _Offset;
            fixed tex = step(0.5, tex2D(_MainTex, uv).b);

            fixed4 c = _GridColor * (1 - saturate(grid)) + tex * _TextureColor;
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = c;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
