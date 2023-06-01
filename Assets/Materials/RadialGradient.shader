Shader "Josep/RadialGradient"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _DistorsionSpeed("Distorsion Speed", float) = 1.0
        _DistorsionStrengh("Distorsion Strengh", float) = 1.0
        _NoiseTexture("Noise Texture", 2D) = "white" {}
        _FilterTexture("Filter Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent"
            "DisableBatching" = "True"
        }
        GrabPass
        {
            "_BackgroundTexture"
        }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            ZTest Less
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolator
            {
                float4 pos : SV_POSITION;
                float2 grabPos : TEXTCOORD0;
                float4 backColor : float4;
            };
        
            sampler2D _NoiseTexture;
            sampler2D _BackgroundTexture;
            Texture2D _MainTex;
            sampler2D _FilterTexture;
            float _DistorsionStrengh;
            float _DistorsionSpeed;

            Interpolator vert (MeshData v)
            {
                Interpolator o;           
                o.pos= UnityObjectToClipPos(v.vertex);
                o.backColor = ComputeGrabScreenPos(o.pos);                
                o.grabPos = v.uv;
                return o;
            }

            fixed4 frag(Interpolator i) : SV_Target
            { 
                float4 noise = tex2Dlod(_NoiseTexture, float4(i.grabPos, 0, 1));
                float4 filter = tex2Dlod(_FilterTexture, float4(i.grabPos, 0, 0));

                i.backColor.x += cos(noise * _Time.x * _DistorsionSpeed) * filter * _DistorsionStrengh;
                i.backColor.y += sin(noise * _Time.x * _DistorsionSpeed) * filter * _DistorsionStrengh;

                //return tex2Dproj(_MainTex, float4(i.backColor, 0, 1));
                //return noise;                
                return tex2Dproj(_BackgroundTexture, i.backColor);
            }
            ENDCG
        }
    }
}
