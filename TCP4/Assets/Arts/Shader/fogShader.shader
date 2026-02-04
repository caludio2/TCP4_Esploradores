Shader "Custom/fogShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (1,1,1,1)
        _FogDensity ("Fog Density", Range(0,1)) = 0.02
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _FogColor;
            float _FogDensity;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float distance = length(_WorldSpaceCameraPos - i.worldPos);

                // Fog factor: clamp between 0 and 1
                float fogFactor = saturate(distance * _FogDensity);

                fixed4 col = tex2D(_MainTex, i.uv);

                col.rgb = lerp(col.rgb, _FogColor.rgb, fogFactor);
                return col;
            }
            ENDCG
        }
    }
}
