// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Forge3D/Force Field Mobile"
{
	Properties
	{			
		_FieldTex ("Field Texture" ,2D) = "" {}
		_InnerTint ("Inner Tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_OuterTint ("Outer Tint", Color) = (1.0, 1.0, 1.0, 1.0)		
		_Offset("Mesh Offset", Float) = 0.02	
		_FieldPanSpeed ("Field Texture Pan Speed", Float) = 1.0			
		_InnterOffset("Mask Offset", Float) = 1.0
		_InnerPow("Mask Feather", Float) = 1.0		
	}

	Category
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend One One	
		Cull Off Lighting Off ZWrite Off Fog {Mode Off}

		SubShader
		{	
			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest	
				#pragma target 3.0

				#include "UnityCG.cginc"

				fixed4 _Pos_0;
				fixed4 _Pos_1;
				fixed4 _Pos_2;
				fixed4 _Pos_3;
				fixed4 _Pos_4;
				fixed4 _Pos_5;				
				
				sampler2D _FieldTex;
				fixed4 _FieldTex_ST;
			
				fixed4 _InnerTint;
				fixed4 _OuterTint;
				
				fixed _FieldPanSpeed;
				fixed _Offset;				
				fixed _InnterOffset;			
				fixed _InnerPow;
			

				struct a2v {					
					fixed4 vertex : POSITION;					
					fixed4 texcoord : TEXCOORD0;
					fixed4 normal : NORMAL;
				};

				struct v2f {
					fixed4 vertex : SV_POSITION;					
					fixed2 uv : TEXCOORD0;					
					fixed4 oPos : TEXCOORD1;
					fixed4 normal: TEXCOORD2;
				};

				fixed2 uvPanner(fixed2 uv, fixed x, fixed y)
				{
					fixed t = _Time;
					return fixed2(uv.x + x * t, uv.y + y * t);
				}

				/// VERTEX
				v2f vert (a2v v)
				{
					v2f o;

					v.vertex.xyz += v.normal * _Offset;
					o.vertex = UnityObjectToClipPos(v.vertex);				
					o.oPos = v.vertex;
					o.normal = v.normal;
					o.uv = TRANSFORM_TEX ( v.texcoord, _FieldTex );					

					return o;
				}

				/// FRAGMENT
				fixed4 frag (v2f i) : COLOR
				{	
					const int interpolators = 6;
					fixed4 pos[6];				
				
					fixed3 innerMask = 0.0; 
					fixed3 outterMask = 0.0;

					pos[0] = _Pos_0;
					pos[1] = _Pos_1;
					pos[2] = _Pos_2;
					pos[3] = _Pos_3;
					pos[4] = _Pos_4;
					pos[5] = _Pos_5;					

					for(int x = 0; x < interpolators; x++)
					{
						fixed dist = distance(pos[x].xyz + i.normal * _Offset, i.oPos.xyz);

						innerMask += saturate((1 - dist * _InnterOffset )) * pos[x].w;
						
					}

					innerMask = saturate(pow(innerMask, _InnerPow));

					fixed field_Tex = tex2D(_FieldTex, uvPanner(i.uv, _FieldPanSpeed, _FieldPanSpeed));
					fixed3 fieldColor = lerp(_OuterTint, _InnerTint, field_Tex.r);
					fixed3 maskColor = lerp(_OuterTint, _InnerTint, innerMask);
					fixed3 final = fieldColor * field_Tex * innerMask + innerMask * maskColor;
					
					return fixed4(final, 1.0);
				}

				ENDCG 

			}	// PASS
		}	// SUBSHADER
	} 	// CATEGORY
}	// SHADER
