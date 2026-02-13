Shader "Unlit/testShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,0,0,1) // Red
        _Color2 ("Color 2", Color) = (0,1,0,1) // Green
        _Color3 ("Color 3", Color) = (0,0,1,1) // Blue
        _Color4 ("Color 4", Color) = (1,1,0,1) // Yellow
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed4 _Color4;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                 fixed4 col = tex2D(_MainTex, i.uv); // Sample the texture

                // Calculate squared distances to each target color
                float d1 = dot(col.rgb - _Color1.rgb, col.rgb - _Color1.rgb);
                float d2 = dot(col.rgb - _Color2.rgb, col.rgb - _Color2.rgb);
                float d3 = dot(col.rgb - _Color3.rgb, col.rgb - _Color3.rgb);
                float d4 = dot(col.rgb - _Color4.rgb, col.rgb - _Color4.rgb);

                fixed4 finalColor = _Color1; // Default to Color 1

                // Find the closest color
                if (d2 < d1 && d2 < d3 && d2 < d4) {
                    finalColor = _Color2;
                } else if (d3 < d1 && d3 < d2 && d3 < d4) {
                    finalColor = _Color3;
                } else if (d4 < d1 && d4 < d2 && d4 < d3) {
                    finalColor = _Color4;
                }
        
                return finalColor;
            }
            ENDCG
        }
    }
}
