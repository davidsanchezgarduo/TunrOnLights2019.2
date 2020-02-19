Shader "Custom/ShadowShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        //_sphere1("Vector",Vector) = (0,0,0)
        _MainTex ("Texture", 2D) = "white" {}
        _ArrayLength("Numero",int) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        /*CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
       // #pragma target 3.0
       
       
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        //half _Glossiness;
        //half _Metallic;
        fixed4 _Color;
        uniform float2 _sphere1;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float d = 0.6;

            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //_sphere1.z = d;
            if(d > 0.5){
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            if(d <= 0.5){
                o.Albedo = (0,0,0);
                o.Alpha = 0.5;
            }
            
        }
        ENDCG*/
        
        Pass{
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            int _ArrayLength = 0;
            float4 _ArrayPoints[50];
            float _ArrayRange[50];
             

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET{
                //float d = distance(i.uv,_sphere1.xy);
                float d = 1;
                fixed4 col;
                col = _Color;
                for(int j=0; j<_ArrayLength; j++){
                    d = distance(i.uv,_ArrayPoints[j].xy);
                    if(d <= _ArrayRange[j]){
                        float per = d/_ArrayRange[j];
                        per*=0.5f;
                        /*if(per>1){
                            per = 1;
                        }*/
                        //per = 1-per;
                        if(col.a > per){
                            col.a = per;
                        }
                    }
                    /*if(d < 0.1){
                        //col.a = 0;
                    }*/
                }
                
                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
