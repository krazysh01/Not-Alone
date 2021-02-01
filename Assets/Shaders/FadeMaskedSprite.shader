Shader "Custom/FadeMaskedSprite"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "black" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _DarkCol ("Darkness Colour", Color) = (0,0,0,1)
        _MaskPow ("Mask Power", Float) = 1
        // [HideInInspector] _FieldOfViewMask ("Field of view Mask", 2D) = "black" {}
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
        
        Stencil {
            Ref 1
            Comp equal
        }

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVertMasked
            #pragma fragment SpriteFragMasked
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            fixed4 _DarkCol;
            half _MaskPow;
            sampler2D _FieldOfViewMask;

            struct v2f_masked
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 screenPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f_masked SpriteVertMasked(appdata_t IN)
            {
                v2f_masked OUT;

                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color * _RendererColor;

                OUT.screenPos = ComputeScreenPos(OUT.vertex);

                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            fixed4 SpriteFragMasked(v2f_masked IN) : SV_Target
            {
                fixed mask = tex2D(_FieldOfViewMask, IN.screenPos).r;
                // return mask;
                mask = pow(mask, _MaskPow);
                mask = 1.0 - mask;

                // return mask;

                fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                c.rgb = lerp(c.rgb, _DarkCol, mask);
                c.rgb *= c.a;

                return c;
            }
        ENDCG
        }
    }
}