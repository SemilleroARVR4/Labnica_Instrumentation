// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// VacuumShaders 2013
// https://www.facebook.com/VacuumShaders
 
Shader "Custom/VertexLit"
{
    Properties {
    _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
 
 
            struct vInput
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };
 
            struct vOutput
            {
                float4 pos :SV_POSITION;
                float4 color :TEXCOORD0;
            };
            fixed4 _Color;
 
            vOutput vert(vInput v)
            {
                vOutput o;
 
                o.pos = UnityObjectToClipPos(v.vertex);
           
                o.color = v.color*_Color;
 
                return o;
            }
 
            float4 frag(vOutput i) : COLOR
            {
                return i.color;
            }
 
            ENDCG
 
        } //Pass
    } //SubShader
} //Shader