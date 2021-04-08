// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASESampleShaders/AnimatedUVDistort"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_DistortTexture("Distort Texture", 2D) = "bump" {}
		_Speed("Speed", Float) = 0
		_UVDistortIntensity("UV Distort Intensity", Range( 0 , 0.04)) = 0.01107169
		_Float0("Float 0", Range( 0 , 1)) = 0
		_Float1("Float 1", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTexture;
		uniform float _Speed;
		uniform sampler2D _DistortTexture;
		uniform float4 _DistortTexture_ST;
		uniform float _UVDistortIntensity;
		uniform float _Float0;
		uniform float _Float1;


		struct Gradient
		{
			int type;
			int colorsLength;
			int alphasLength;
			float4 colors[8];
			float2 alphas[8];
		};


		Gradient NewGradient(int type, int colorsLength, int alphasLength, 
		float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
		float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
		{
			Gradient g;
			g.type = type;
			g.colorsLength = colorsLength;
			g.alphasLength = alphasLength;
			g.colors[ 0 ] = colors0;
			g.colors[ 1 ] = colors1;
			g.colors[ 2 ] = colors2;
			g.colors[ 3 ] = colors3;
			g.colors[ 4 ] = colors4;
			g.colors[ 5 ] = colors5;
			g.colors[ 6 ] = colors6;
			g.colors[ 7 ] = colors7;
			g.alphas[ 0 ] = alphas0;
			g.alphas[ 1 ] = alphas1;
			g.alphas[ 2 ] = alphas2;
			g.alphas[ 3 ] = alphas3;
			g.alphas[ 4 ] = alphas4;
			g.alphas[ 5 ] = alphas5;
			g.alphas[ 6 ] = alphas6;
			g.alphas[ 7 ] = alphas7;
			return g;
		}


		float4 SampleGradient( Gradient gradient, float time )
		{
			float3 color = gradient.colors[0].rgb;
			UNITY_UNROLL
			for (int c = 1; c < 8; c++)
			{
			float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1));
			color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
			}
			#ifndef UNITY_COLORSPACE_GAMMA
			color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
			#endif
			float alpha = gradient.alphas[0].x;
			UNITY_UNROLL
			for (int a = 1; a < 8; a++)
			{
			float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1));
			alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
			}
			return float4(color, alpha);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			Gradient gradient27 = NewGradient( 0, 2, 2, float4( 1, 1, 1, 0 ), float4( 0, 1, 0.8728237, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float mulTime10 = _Time.y * _Speed;
			float2 panner11 = ( mulTime10 * float2( -1,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord9 = i.uv_texcoord + panner11;
			float2 uv_DistortTexture = i.uv_texcoord * _DistortTexture_ST.xy + _DistortTexture_ST.zw;
			float3 tex2DNode6 = UnpackScaleNormal( tex2D( _DistortTexture, uv_DistortTexture ), _UVDistortIntensity );
			float mulTime15 = _Time.y * _Speed;
			float2 panner17 = ( mulTime15 * float2( 1,0.5 ) + float2( 0,0 ));
			float2 uv_TexCoord18 = i.uv_texcoord + panner17;
			float4 temp_output_8_0 = ( tex2D( _MainTexture, ( float3( uv_TexCoord9 ,  0.0 ) + tex2DNode6 ).xy ) * tex2D( _MainTexture, ( float3( uv_TexCoord18 ,  0.0 ) + tex2DNode6 ).xy ) );
			float4 temp_output_20_0 = ( SampleGradient( gradient27, temp_output_8_0.r ) * temp_output_8_0 );
			o.Albedo = temp_output_20_0.rgb;
			o.Emission = temp_output_20_0.rgb;
			o.Metallic = _Float0;
			o.Smoothness = _Float1;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
284;73;1244;695;879.6919;588.431;1.241893;True;False
Node;AmplifyShaderEditor.RangedFloatNode;23;-2400.123,58.86468;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;15;-1722.107,490.1429;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1643.784,-111.7641;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;11;-1449.904,-251.4571;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;17;-1493.972,362.5278;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1916.708,106.293;Float;False;Property;_UVDistortIntensity;UV Distort Intensity;4;0;Create;True;0;0;0;False;0;False;0.01107169;0;0;0.04;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1558.098,90.11827;Inherit;True;Property;_DistortTexture;Distort Texture;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1225.597,-252.8895;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1272.574,354.2421;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1013.526,448.6211;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-978.4656,-159.0065;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-856.8282,-182.0895;Inherit;True;Property;_MainTexture;Main Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-850.0068,1.76078;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-531.6471,-106.887;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;27;-598.4784,-492.015;Inherit;False;0;2;2;1,1,1,0;0,1,0.8728237,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.GradientSampleNode;29;-354.7025,-418.9112;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-851.9389,-413.8653;Float;False;Property;_TintColor;Tint Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,0.4196078,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-42.04967,-257.8561;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-40.5348,-8.469933;Inherit;False;Property;_Float0;Float 0;5;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-37.93477,90.33009;Inherit;False;Property;_Float1;Float 1;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;267.9615,-227.9616;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;ASESampleShaders/AnimatedUVDistort;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;23;0
WireConnection;10;0;23;0
WireConnection;11;1;10;0
WireConnection;17;1;15;0
WireConnection;6;5;13;0
WireConnection;9;1;11;0
WireConnection;18;1;17;0
WireConnection;19;0;18;0
WireConnection;19;1;6;0
WireConnection;12;0;9;0
WireConnection;12;1;6;0
WireConnection;1;1;12;0
WireConnection;14;1;19;0
WireConnection;8;0;1;0
WireConnection;8;1;14;0
WireConnection;29;0;27;0
WireConnection;29;1;8;0
WireConnection;20;0;29;0
WireConnection;20;1;8;0
WireConnection;0;0;20;0
WireConnection;0;2;20;0
WireConnection;0;3;24;0
WireConnection;0;4;25;0
ASEEND*/
//CHKSM=7EF9BCBCF45AF19C304ACDE4CE421DECA3D21A5C