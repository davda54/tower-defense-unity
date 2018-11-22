Shader "Custom/CircleOnTop" {
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		_Thickness("Thickness", Range(0.0,2)) = 0.05
		_Radius("Radius", Range(0.0, 2)) = 0.4
		_Dropoff("Dropoff", Range(0.01, 4)) = 0.1

		[HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask("Color Mask", Float) = 15
		[HideInInspector][Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}

		SubShader{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile __ UNITY_UI_ALPHACLIP

				struct appdata_t {
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				float _Thickness;
				float _Radius;
				float _Dropoff;

				fixed4 _TextureSampleAdd;
				float4 _ClipRect;

				v2f vert(appdata_t IN) {
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(IN);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

					OUT.worldPosition = IN.vertex;
					OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color;

					return OUT;
				}

				float antialias(float radius, float dist, float thick, float drop) {
					if(dist < (radius - 0.5*thick))
						return 1 - pow(dist - radius + 0.5*thick,2) / pow(drop*thick, 2);
					else if(dist > (radius + 0.5*thick))
						return 1 - pow(dist - radius - 0.5*thick,2) / pow(drop*thick, 2);
					else
						return 1;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					half4 color;

					float distance = sqrt(pow(IN.texcoord.x - 0.5, 2) + pow(IN.texcoord.y - 0.5,2));
					float alias = antialias(_Radius, distance, _Thickness, _Dropoff);

					if(alias != 1) {
						color = tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd;
						color.a *= IN.color.a;

						#ifdef UNITY_UI_ALPHACLIP
						clip(color.a - 0.001);
						#endif
					} else {
						color = IN.color;
						color.a = IN.color.a * alias;
					}

					return color;
				}

			ENDCG
			}
		}
}