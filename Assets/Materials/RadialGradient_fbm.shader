Shader "Josep/RadialGradientFBM"
{
    Properties
    {     
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
        
            sampler2D _BackgroundTexture;
            sampler2D _FilterTexture;

            float random(float2 input) 
            {
                return frac(sin(dot(input.xy,
                    float2(12.9898, 78.233))) *
                    43758.5453123);
            }

            // Based on Morgan McGuire @morgan3d
            // https://www.shadertoy.com/view/4dS3Wd
            float noise(float2 input) 
            {
                float2 i = floor(input);
                float2 f = frac(input);

                // Four corners in 2D of a tile
                float a = random(i);
                float b = random(i + float2(1.0, 0.0));
                float c = random(i + float2(0.0, 1.0));
                float d = random(i + float2(1.0, 1.0));

                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(a, b, u.x) +
                    (c - a) * u.y * (1.0 - u.x) +
                    (d - b) * u.x * u.y;
            }

            #define NUM_OCTAVES 5
            float fbm(float2 input) 
            {
                float v = 0.0;
                float a = 0.5;
                float2 shift = float2(100.0,0);
                // Rotate to reduce axial bias
                float2x2 rot = float2x2(cos(0.5), sin(0.5),
                    -sin(0.5), cos(0.50));
                for (int i = 0; i < NUM_OCTAVES; ++i) {
                    v += a * noise(input);
                    input = mul(rot, input) * 2.0 + shift;
                    a *= 0.5;
                }
                return v;
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
                float3 color = float3(0.0, 0.0, 0.0);
                float2 q = float2(0.0, 0.0);

                q.x = fbm(st + 0.01 * _Time.x);
                q.y = fbm(st + float2(1.0, 0.0));

                float2 r = float2(0.0, 0.0);
                r.x = fbm(st + 1.0 * q + float2(1.7, 9.2) + 2.0 * _Time.x);
                r.y = fbm(st + 1.0 * q + float2(8.3, 2.8) + 5.0 * _Time.x);

                float f = fbm(st + r);
                color = lerp(float3(0.101961, 0.619608, 0.666667),
                            float3(0.666667,0.666667,0.498039),
                            clamp((f * f) * 4.0,0.0,1.0));

                color = lerp(color,
                            float3(0.165,0.062,0.122),
                            clamp(length(q),0.0,1.0));

                color = lerp(color,
                            float3(0.666667,1,1),
                            clamp(length(r.x),0.0,1.0));

                float4 finalNoise = float4((f * f * f + .6 * f * f + .5 * f) * color, 1.);
                float4 filter = tex2Dlod(_FilterTexture, float4(i.grabPos, 0, 0));

                i.backColor.x += finalNoise * filter;
                i.backColor.y += finalNoise * filter;

                return tex2Dproj(_BackgroundTexture, i.backColor);
            }
            ENDCG
        }
    }
}
