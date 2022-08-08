// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FX/Waves/Wave Creator Mobile" {

	Properties {
	
		_DeepColor ("Deep Water Color", Color) = (1,1,1,1)
		_ShallowColor ("Shallow Water Color", Color) = (1,1,1,1)
		_HeightAdjustment ("Shallow to Deep Adjuster", Range (1,2)) = 1.0
		
		_SpecColor ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess ("Shininess", Float) = 150.0
	
		_MainTex ("Water Texture", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}

		_ReflectPow ("Reflection Power", Range (0,1)) = 0.1
		_Cube ("Cube Map", Cube) = "" {}
		
		_FoamColor ("Foam Color", Color) = (1,1,1,1)
		_FoamTex ("Foam Texture", 2D) = "white" {}
		_ShorelineFoamAdjustment ("Shoreline Foam Adjustment", Range(1,2)) = 1.2
		_WaveFoamAdjustment ("Wave Foam Adjustment", Range(-1,1)) = 0.0
		
		_Amp ("Wave Height" , Float) = 1
		_Steepness ("Wave Steepness" , Range(2,5)) = 2.5
		_Freq ("Wave frequency" , Range(0,1)) = 0.5
		_Velocity ("Wave velocity" , Float) = 10
		_DirectionType ("Direction Type" , Float) = 1.0
		_Dirx ("X Direction", Float) = 1
		_Dirz ("Z Direction", Float) = 1
		_WaveFadeout ("Wave Fadeout", Float) = 25.0
		
		_Heightmap ("Heightmap of the terrain", 2D) = "white" {}
		
		// Unseen properties
		_TerrainMaxHeight ("", Float) = 15.0
		_CamPos ("", Vector) = (0.0,0.0,0.0,0.0)
		
		
	}
	
	SubShader {
		
		Cull Off
		Tags { "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
		
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
				uniform half4 _DeepColor;
				uniform half4 _ShallowColor;
				uniform half _HeightAdjustment;
				
				uniform half4 _SpecColor;
				uniform half _Shininess;
				
				uniform sampler2D _MainTex;
				uniform half4 _MainTex_ST; 
				uniform sampler2D _BumpMap;
				uniform half4 _BumpMap_ST;
				
				uniform half _ReflectPow;
				uniform samplerCUBE _Cube;
				
				uniform half4 _FoamColor;
				uniform sampler2D _FoamTex;
				uniform half4 _FoamTex_ST; 
				uniform half _ShorelineFoamAdjustment;
				uniform half _WaveFoamAdjustment;

				uniform half _Amp;
				uniform half _Steepness;
				uniform half _Freq;
				uniform half _Velocity;
				uniform fixed _DirectionType;
				uniform half _Dirx;
				uniform half _Dirz;
				uniform half _WaveFadeout;
				
				uniform sampler2D _Heightmap;
				uniform half4 _Heightmap_ST;
				
				uniform half _TerrainMaxHeight;
				uniform half4 _CamPos;
				
				uniform half4 _LightColor0;
				
			struct vertexInput
			{
				half4 vertex : POSITION;
				half4 texcoord : TEXCOORD0;
				half3 normal : NORMAL;
				half4 tangent : TANGENT;
			};
			
			struct vertexOutput 
			{
				half4 pos : SV_POSITION;
				half4 tex : TEXCOORD0;
				half4 pos_world : TEXCOORD1;
				half3 tangent_world : TEXCOORD2;
				half3 normal_world : TEXCOORD3;
				half3 binormal_world : TEXCOORD4;
				half3 height_properties : TEXCOORD5;
//				half4 light_direction : TEXCOORD6;
				half3 view_direction : TEXCOORD7;
			};
			
			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;
				
				half4 new_position = v.vertex;
				
				float view_distance = distance (mul(unity_ObjectToWorld, new_position), _CamPos); 
				_Amp = max( 0.0, (_Amp - ((_Amp * view_distance) / _WaveFadeout)));
				
				o.height_properties = half3 (0, 0, 0);
				
				if (_Amp > 0) {
				
					half amp_array [4] = {1,0.5,0.3,0.2};
					half steepness_array [4] = {1,0.4,0.6,0.4};
					half freq_array [4] = {1,0.5,2,2.5};
					half velocity_array [4] = {1,0.5,1.2,1.2};
					half2 dir_array [4] = {half2 (_Dirx, _Dirz), half2 (1,1), half2 (1,0), half2 (0,1)};
					
					for (int i = 0; i < 4; i++) {
					
						half2 flat_pos = half2 (new_position.x, new_position.z) - lerp(half2 (0,0), half2 (_Dirx + i, _Dirz - i), _DirectionType.x);
						half2 dir = lerp (half2 (dir_array[i].x, dir_array[i].y), normalize (flat_pos.xy), _DirectionType.x);

						new_position.y += (_Amp * amp_array[i]) * pow ((sin ((_Freq * freq_array[i] * dot (dir.xy, flat_pos.xy)) + (_Time.x * _Velocity * velocity_array[i])) + 1) / 2, 1 + (_Steepness * steepness_array[i]));
					}
					
					o.height_properties.x = new_position.y / 2;
					o.height_properties.y = o.height_properties.x / _Amp;
					o.height_properties.z = saturate (10 * (o.height_properties.y + _WaveFoamAdjustment));
				}
				
				o.pos_world = mul(unity_ObjectToWorld, new_position);
				o.tangent_world = normalize (mul (unity_ObjectToWorld, v.tangent).xyz);
				o.normal_world = normalize( mul( half4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.binormal_world = normalize (cross (o.normal_world, o.tangent_world) * v.tangent.w);
				
				o.pos = UnityObjectToClipPos(new_position); 
				o.tex = v.texcoord;
				
					o.view_direction = normalize (_CamPos.xyz - o.pos_world.xyz);
//					half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - o.pos_world.xyz;
//					o.light_direction = half4 (normalize (lerp (_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w)), lerp(1.0 , 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w));
				
				return o;
			}
			
			half4 frag(vertexOutput i) : COLOR
			{
				// Movement
				half2 water_movement = half2(-_Time.x, -_Time.x);
			
				// Colours
				half4 heightmap = tex2D(_Heightmap, i.tex.xy);
				half heightmap_adjustment = heightmap.a - (i.pos_world.y / _TerrainMaxHeight);
				half4 final_color = lerp (_DeepColor, _ShallowColor, saturate (pow (_HeightAdjustment + heightmap_adjustment, 5)));
				final_color.rgb += i.height_properties.y / 2;
				final_color.a += i.height_properties.y;
				
				// Foam
				half2 foam_mapping = _FoamTex_ST.xy * i.tex.xy;
				half4 foam_layer_1 = tex2D(_FoamTex, water_movement + foam_mapping);
				half4 foam_layer_2 = tex2D(_FoamTex, -water_movement + foam_mapping + half2(0.5,0));
				half foam_spread_1 = saturate (pow (_ShorelineFoamAdjustment + heightmap_adjustment, 10) + i.height_properties.z);
				half4 foam_final = (foam_layer_1 + foam_layer_2) * _FoamColor * 0.5;
			
				// Normals
//				half2 normal_mapping = i.tex.xy * _BumpMap_ST.xy;
//				
//				half4 normal_map_1 = tex2D(_BumpMap, water_movement + normal_mapping);
//				half3 local_coords_1 = half3 (2.0 * normal_map_1.ag - half2(1.0, 1.0), 1.0);
//					
//				half4 normal_map_2 = tex2D(_BumpMap, -water_movement + normal_mapping);
//				half3 local_coords_2 = half3 (2.0 * normal_map_2.ag - half2(1.0, 1.0), 1.0);
//
//				half3 local_coords_final = local_coords_1 * local_coords_2;
				
				// Normal transpose matrix
//				half3x3 local_2_world_transpose = half3x3 (i.tangent_world, i.binormal_world, i.normal_world);
			
				// Lighting
//				half3 normal_direction = normalize (mul (local_coords_final.xyz, local_2_world_transpose));
//				half3 diffuse_reflection = max(0.0, dot (normal_direction.xyz, i.light_direction.xyz));
//				half3 specular_reflection =  lerp (10 , 0, foam_spread_1) * diffuse_reflection.rgb * _SpecColor.rgb * pow (max (0.0, dot (reflect(-i.light_direction, normal_direction), i.view_direction)), _Shininess);
//				half3 lighting_final = half3 (_LightColor0.rgb * (diffuse_reflection.rgb + specular_reflection.rgb + UNITY_LIGHTMODEL_AMBIENT.rgb));
			
				// Textures
				half4 water_layer = tex2D(_MainTex, water_movement + _MainTex_ST.xy * i.tex.xy);
				half4 textures_final = lerp (final_color * water_layer, 1.5 * foam_final, foam_spread_1 * foam_final.a);
				// half4 textures_final = final_color * water_layer;
				
				half3 reflect_dir = reflect (i.view_direction, half3(0,0,-1));
				half4 cube_map = texCUBE(_Cube, reflect_dir);
				return half4 (lerp (1.5 * _LightColor0.rgb * textures_final.rgb, cube_map.rgb, _ReflectPow), final_color.a);
				
				/////////////////////////////////////////////////////////////////////////////////////////////////
			}
			
			ENDCG
		}
	}
	
	// Comment out during development
	FallBack "Diffuse"
	CustomEditor "WaveCreatorEditor"
}