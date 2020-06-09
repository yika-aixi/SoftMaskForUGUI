Shader "Hidden/UI/Default (SoftMaskable)"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
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

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP

            #include "Packages/com.coffee.softmask-for-ugui/Shaders/SoftMask.cginc"	// Add for soft mask
            #pragma shader_feature __ SOFTMASK_EDITOR	// Add for soft mask

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
            {
                v2f OUT;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;

                return OUT;
            }

            UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);

            fixed4 frag(v2f IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
                //half2 viewa = IN.vertex.xy/_ScreenParams.xy;

                //fixed4 c = lerp(tex2D(_SoftMaskTex, viewa), tex2D(_SoftMaskTex2, viewa), unity_StereoEyeIndex);
                //viewa = lerp(viewa, viewa - half2(1, 0), unity_StereoEyeIndex);
                //fixed4 c = unity_StereoEyeIndex;
                //c.a = 1;
               // c.rg = viewa;
               // c.b = 0;
                //return c;

                half4 color = (UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

                #ifdef UNITY_UI_ALPHACLIP
                    clip (color.a - 0.001);
                #endif

                color.a *= SoftMask(IN.vertex, IN.worldPosition);	// Add for soft mask

                return color;
            }
            ENDCG
        }
    }
}
