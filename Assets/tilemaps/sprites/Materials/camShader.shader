Shader "Custom/camShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,0,0,1)
        _Color2 ("Color 2", Color) = (0,1,0,1)
        _Color3 ("Color 3", Color) = (0,0,1,1)
        _Color4 ("Color 4", Color) = (1,1,0,1) 
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed4 _Color4;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors

                float d1 = dot(col.rgb - _Color1.rgb, col.rgb - _Color1.rgb);
                float d2 = dot(col.rgb - _Color2.rgb, col.rgb - _Color2.rgb);
                float d3 = dot(col.rgb - _Color3.rgb, col.rgb - _Color3.rgb);
                float d4 = dot(col.rgb - _Color4.rgb, col.rgb - _Color4.rgb);

                col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
