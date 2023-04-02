Shader "Unlit/Accelerate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MaskTex ("Mask", 2D) = "white" {}

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
				float2 uv_mask: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			sampler2D _MaskTex;
			float4 _MaskTex_ST;

            fixed4 _TintColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv_mask = TRANSFORM_TEX(v.uv, _MaskTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
				//i.uv.y -= _Time.y * 2;
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 mask = tex2D(_MaskTex, i.uv_mask);
                col *= _TintColor;
				col.a *= mask.a;
                col *= 3;
                return col;
            }
            ENDCG
        }
    }
}
