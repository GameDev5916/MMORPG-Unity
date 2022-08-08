// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Forge3D/Force Field"
{
	Properties
	{			
		_FieldTex ("Field Texture" ,2D) = "" {}
		_InnerTint ("Inner Mask Tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_OuterTint ("Outer Mask Tint", Color) = (1.0, 1.0, 1.0, 1.0)		
		_Offset("Mesh Offset", Float) = 0.02	
		_FieldPanSpeed ("Field Texture Pan Speed", Float) = 1.0	
		_FieldBG ("Field Background Visibility", Float) = 1.0	
		_FieldSparks ("Field Sparks Visibility", Float) = 1.0				
		_InnterOffset("Inner Mask Offset", Float) = 1.0
		_InnerPow("Inner Mask Feather", Float) = 1.0
		_OutterOffset("Outter Mask Offset", Float) = 1.0		
		_OutterPow("Outter Mask Feather", Float) = 1.0
	}

	Category
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend One One
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog {Mode Off}

		SubShader
		{	
			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#if defined(SHADER_API_D3D9) || defined(SHADER_API_D3D11)
					#pragma target 3.0
				#else
					#pragma glsl
				#endif

				#include "UnityCG.cginc"

				fixed4 _Pos_0;
				fixed4 _Pos_1;
				fixed4 _Pos_2;
				fixed4 _Pos_3;
				fixed4 _Pos_4;
				fixed4 _Pos_5;
				fixed4 _Pos_6;
				fixed4 _Pos_7;
				fixed4 _Pos_8;
				fixed4 _Pos_9;
				fixed4 _Pos_10;
				fixed4 _Pos_11;
				fixed4 _Pos_12;	
				fixed4 _Pos_13;
				fixed4 _Pos_14;
				fixed4 _Pos_15;
				fixed4 _Pos_16;
				fixed4 _Pos_17;
				fixed4 _Pos_18;
				fixed4 _Pos_19;
				fixed4 _Pos_20;
				fixed4 _Pos_21;
				fixed4 _Pos_22;
				fixed4 _Pos_23;

				fixed _Pow_0;
				fixed _Pow_1;
				fixed _Pow_2;
				fixed _Pow_3;
				fixed _Pow_4;
				fixed _Pow_5;
				fixed _Pow_6;
				fixed _Pow_7;
				fixed _Pow_8;
				fixed _Pow_9;
				fixed _Pow_10;
				fixed _Pow_11;
				fixed _Pow_12;
				fixed _Pow_13;
				fixed _Pow_14;
				fixed _Pow_15;
				fixed _Pow_16;
				fixed _Pow_17;
				fixed _Pow_18;
				fixed _Pow_19;
				fixed _Pow_20;
				fixed _Pow_21;
				fixed _Pow_22;
				fixed _Pow_23;
				

				sampler2D _FieldTex;
				fixed4 _FieldTex_ST;
			
				fixed4 _InnerTint;
				fixed4 _OuterTint;

				fixed _FieldBG;
				fixed _FieldSparks;				
				fixed _FieldPanSpeed;
				fixed _Offset;				
				fixed _InnterOffset;
				fixed _OutterOffset;
				fixed _InnerPow;
				fixed _OutterPow;

				struct a2v {					
					fixed4 vertex : POSITION;					
					fixed4 texcoord : TEXCOORD0;
					fixed4 normal : NORMAL;
				};

				struct v2f {
					fixed4 vertex : SV_POSITION;					
					fixed2 uv : TEXCOORD0;					
					fixed4 oPos : TEXCOORD2;
					fixed4 normal: TEXCOORD3;
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
					int interpolators = 24;
					fixed4 pos[24];	
					fixed power[24];			
				
					fixed3 innerMask = 0.0; 
					fixed3 outterMask = 0.0;

					pos[0] = _Pos_0;
					pos[1] = _Pos_1;
					pos[2] = _Pos_2;
					pos[3] = _Pos_3;
					pos[4] = _Pos_4;
					pos[5] = _Pos_5;
					pos[6] = _Pos_6;
					pos[7] = _Pos_7;
					pos[8] = _Pos_8;
					pos[9] = _Pos_9;
					pos[10] = _Pos_10;
					pos[11] = _Pos_11;
					pos[12] = _Pos_12;
					pos[13] = _Pos_13;
					pos[14] = _Pos_14;
					pos[15] = _Pos_15;
					pos[16] = _Pos_16;
					pos[17] = _Pos_17;
					pos[18] = _Pos_18;
					pos[19] = _Pos_19;
					pos[20] = _Pos_20;
					pos[21] = _Pos_21;
					pos[22] = _Pos_22;
					pos[23] = _Pos_23;

					power[0] = _Pow_0;
					power[1] = _Pow_1;
					power[2] = _Pow_2;
					power[3] = _Pow_3;
					power[4] = _Pow_4;
					power[5] = _Pow_5;
					power[6] = _Pow_6;
					power[7] = _Pow_7;
					power[8] = _Pow_8;
					power[9] = _Pow_9;
					power[10] = _Pow_10;
					power[11] = _Pow_11;
					power[12] = _Pow_12;
					power[13] = _Pow_13;
					power[14] = _Pow_14;
					power[15] = _Pow_15;
					power[16] = _Pow_16;
					power[17] = _Pow_17;
					power[18] = _Pow_18;
					power[19] = _Pow_19;
					power[20] = _Pow_20;
					power[21] = _Pow_21;
					power[22] = _Pow_22;
					power[23] = _Pow_23;


					for(int x = 0; x < interpolators; x++)
					{
						fixed dist = distance(pos[x].xyz + i.normal * _Offset, i.oPos.xyz);

						innerMask += pow(saturate(1 - dist * ((power[x] + _InnterOffset) + (1 - pos[x].w) * 2)), _InnerPow) * pos[x].w;
						outterMask += pow(saturate(1 - dist * (_OutterOffset - (1 - pos[x].w) * 2)), _OutterPow) * pos[x].w;
					}
										
					fixed field_Tex = pow(tex2D(_FieldTex, uvPanner(i.uv, _FieldPanSpeed, _FieldPanSpeed)), 2.2).r;
					fixed field_Tex_inv = pow(tex2D(_FieldTex, uvPanner(i.uv, -_FieldPanSpeed, -_FieldPanSpeed)), 2.2).r;
										
				
					fixed3 fieldColor = lerp(_OuterTint, _InnerTint, field_Tex.r).rgb;
					fixed sparksMask = saturate(field_Tex * field_Tex_inv).x;
					fixed3 sparks = saturate(sparksMask * fieldColor) * _FieldSparks;		
					
					outterMask = saturate(outterMask * _OuterTint.rgb);
					innerMask = saturate(innerMask * _InnerTint.rgb);
										
					fixed3 final = outterMask * _OuterTint.a * 10 + innerMask * _InnerTint.a * 10;
					final += field_Tex * fieldColor * outterMask * _FieldBG;
					final += sparks * outterMask * 10;
					
					return fixed4(final, 1.0);
				}

				ENDCG 

			}	// PASS
		}	// SUBSHADER
	} 	// CATEGORY
}	// SHADER
