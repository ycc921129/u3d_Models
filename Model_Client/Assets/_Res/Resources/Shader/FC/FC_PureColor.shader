// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// FC ¾«Áé´¿É«Shader
Shader "[FC_Shader]/Sprites/FC_PureColor"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent-1"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

        Pass
        {

        CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };
            
            fixed4 _Color;
            sampler2D _MainTex;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                UNITY_INITIALIZE_OUTPUT(v2f, OUT);

                //OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.vertex = mul(unity_ObjectToWorld, IN.vertex);
                OUT.vertex.z = 0;
                OUT.vertex = mul(UNITY_MATRIX_VP, OUT.vertex);

                OUT.texcoord = IN.texcoord;
                //OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                //fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                //c.rgb *= c.a;

                fixed4 c = tex2D(_MainTex, IN.texcoord);
                c.rgba = _Color * c.a;
                c.rgba *= _Color.a;
                return c;
            }

        ENDCG

        }
    }
}