// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:34326,y:32673,varname:node_9361,prsc:2|normal-5506-RGB,custl-8112-OUT,olwid-9533-OUT;n:type:ShaderForge.SFN_NormalVector,id:4178,x:32615,y:32842,prsc:2,pt:True;n:type:ShaderForge.SFN_Transform,id:9092,x:32850,y:32843,varname:node_9092,prsc:2,tffrom:0,tfto:3|IN-4178-OUT;n:type:ShaderForge.SFN_ComponentMask,id:696,x:33082,y:32861,varname:node_696,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9092-XYZ;n:type:ShaderForge.SFN_RemapRange,id:890,x:33312,y:32846,varname:node_890,prsc:0,frmn:-1,frmx:1,tomn:0,tomx:1|IN-696-OUT;n:type:ShaderForge.SFN_Tex2d,id:6408,x:33530,y:32860,ptovrint:False,ptlb:Matcap texture,ptin:_Matcaptexture,varname:_Matcaptexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:20eeca7a73a9b3a4d875f843ff932ba3,ntxv:0,isnm:False|UVIN-890-OUT;n:type:ShaderForge.SFN_Tex2d,id:5506,x:33530,y:32604,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:_Normal,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_SwitchProperty,id:9533,x:33521,y:33165,ptovrint:False,ptlb:EnableOutline,ptin:_EnableOutline,varname:_EnableOutline,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-9119-OUT,B-8665-OUT;n:type:ShaderForge.SFN_Vector1,id:9119,x:33273,y:33130,varname:node_9119,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:5673,x:32781,y:33312,ptovrint:False,ptlb:Outline width,ptin:_Outlinewidth,varname:_Outlinewidth,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Vector1,id:3106,x:32959,y:33482,varname:node_3106,prsc:0,v1:10;n:type:ShaderForge.SFN_Divide,id:8665,x:33225,y:33495,varname:node_8665,prsc:2|A-5673-OUT,B-3106-OUT;n:type:ShaderForge.SFN_Multiply,id:8112,x:33879,y:32796,varname:node_8112,prsc:2|A-1342-RGB,B-6408-RGB;n:type:ShaderForge.SFN_Tex2d,id:1342,x:33532,y:32364,ptovrint:False,ptlb:Color texture,ptin:_Colortexture,varname:_Colortexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:6408-1342-5506-9533-5673;pass:END;sub:END;*/

Shader "Jonathan3D/Matcap Basic" {
    Properties {
        _Matcaptexture ("Matcap texture", 2D) = "white" {}
        _Colortexture ("Color texture", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        [MaterialToggle] _EnableOutline ("EnableOutline", Float ) = 0
        _Outlinewidth ("Outline width", Range(0, 1)) = 0.1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform fixed _EnableOutline;
            uniform fixed _Outlinewidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*lerp( 0.0, (_Outlinewidth/10.0), _EnableOutline ),1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(float3(0,0,0),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Matcaptexture; uniform float4 _Matcaptexture_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Colortexture; uniform float4 _Colortexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
                float4 _Colortexture_var = tex2D(_Colortexture,TRANSFORM_TEX(i.uv0, _Colortexture));
                fixed2 node_890 = (mul( UNITY_MATRIX_V, float4(normalDirection,0) ).xyz.rgb.rg*0.5+0.5);
                float4 _Matcaptexture_var = tex2D(_Matcaptexture,TRANSFORM_TEX(node_890, _Matcaptexture));
                float3 finalColor = (_Colortexture_var.rgb*_Matcaptexture_var.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
