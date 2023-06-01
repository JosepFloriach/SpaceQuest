Shader "Josep/RadialGradient_Lava"
{
    Properties
    {
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
        ;
            sampler2D _BackgroundTexture;
            sampler2D _FilterTexture;

            #define mod(x, y) (x - y * floor(x / y))

            float4 permute(float4 x) {
                return mod((34.0 * x + 1.0) * x, 289.0);
            }

            float2 cellular2x2(float2 P) 
            {
                #define K 0.142857142857 // 1/7
                #define K2 0.0714285714285 // K/2
                #define jitter 0.8 // jitter 1.0 makes F1 wrong more often
                float2 Pi = mod(floor(P), 289.0);
                float2 Pf = frac(P);
                float4 Pfx = Pf.x + float4(-0.5, -1.5, -0.5, -1.5);
                float4 Pfy = Pf.y + float4(-0.5, -0.5, -1.5, -1.5);
                float4 p = permute(Pi.x + float4(0.0, 1.0, 0.0, 1.0));
                p = permute(p + Pi.y + float4(0.0, 0.0, 1.0, 1.0));
                float4 ox = mod(p, 7.0) * K + K2;
                float4 oy = mod(floor(p * K), 7.0) * K + K2;
                float4 dx = Pfx + jitter * ox;
                float4 dy = Pfy + jitter * oy;
                float4 d = dx * dx + dy * dy; // d11, d12, d21 and d22, squared
                // Sort out the two smallest distances
                #if 0
                    // Cheat and pick only F1
                                d.xy = min(d.xy, d.zw);
                                d.x = min(d.x, d.y);
                                return d.xx; // F1 duplicated, F2 not computed
                #else
                    // Do it right and find both F1 and F2
                                d.xy = (d.x < d.y) ? d.xy : d.yx; // Swap if smaller
                                d.xz = (d.x < d.z) ? d.xz : d.zx;
                                d.xw = (d.x < d.w) ? d.xw : d.wx;
                                d.y = min(d.y, d.z);
                                d.y = min(d.y, d.w);
                                return sqrt(d.xy);
                #endif
            }

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
                float2 st = i.grabPos;
                float2 F = cellular2x2(st * 19.408);

                float2 pos = st - .5;
                float a = dot(pos,pos) - _Time.x * -2.092;
                float n = step(abs(sin(a * 3.1415 * 5.256)), F.x * 1.984);

                //return float4(n, n, n, 1.0);

                float4 filter = tex2Dlod(_FilterTexture, float4(i.grabPos, 0, 0));

                i.backColor.x += filter * n;
                i.backColor.y += filter * n;
               
                return tex2Dproj(_BackgroundTexture, i.backColor);
            }
            ENDCG
        }
    }
}
