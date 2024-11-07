Shader "Custom/SeaShader"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.0, 0.5, 0.7, 1.0)
        _WaveFrequency ("Wave Frequency", Float) = 5.0
        _WaveHeight ("Wave Height", Float) = 0.05
        _WaveSpeed ("Wave Speed", Float) = 0.25
        _ReflectionStrength ("Reflection Strength", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        struct Input
        {
            float2 uv_MainTex;
        };

        float _WaveFrequency;
        float _WaveHeight;
        float _WaveSpeed;
        float _ReflectionStrength;
        fixed4 _Color;

        void vert (inout appdata_full v)
        {
            v.vertex.y += sin(v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveHeight;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
