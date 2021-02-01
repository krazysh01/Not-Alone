Shader "Custom/AdditiveVertexAlpha"
{
    Properties
    {
    }
    SubShader
    {
        Tags 
        { 
        	"RenderType"="Transparent"
        }
        LOD 100
        ZWrite Off
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 col: COLOR0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 col: COLOR0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.col = v.col;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.col.a;
            }
            ENDCG
        }
    }
}
