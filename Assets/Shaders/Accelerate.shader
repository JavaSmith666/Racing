Shader "Unlit/Accelerate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _TintColor("TintColor", Color) = (0.5, 0.5, 0.5, 0.5)
    }
    SubShader
    {
        Tags { "RenderType"="Transparnet" "Queue"="Transparent"}
        Blend SrcAlpha One
        cull off
        ZWrite off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _TintColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = (0.5, 0.5);
                float2 des = normalize(i.uv - center) * _Time.y * 0.1 + center;
                if(abs(center.x - des.x) > 0.5 || abs(center.y - des.y) > 0.5) {
                    des = center;
                }
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _TintColor;
                col *= 3;
                return col;
            }
            ENDCG
        }
    }
}
