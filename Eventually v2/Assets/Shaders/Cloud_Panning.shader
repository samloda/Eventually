Shader "OpenGL/Panning" 
{
	Properties
	{
		_Color ("Ambient Color", Color) = (1.0,1.0,1.0,1.0)
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_Brightness("Brightness (RGB)", float) = 1.0
	}
	SubShader
	{
	Tags{"Queue" = "Geometry"}
		
		
		Pass
		{
			GLSLPROGRAM
			
			uniform vec4 _Color;
			uniform sampler2D _MainTex;
			uniform vec4 _Time;
			uniform float _Brightness;
			
			varying vec4 vertexColor;
			varying vec4 vertexData;
			varying vec2 uv;
			
			#ifdef VERTEX
			
			void main()
			{
			 vertexColor = _Color / _Brightness;
			 uv = gl_MultiTexCoord0;
			 
			 vertexData = gl_Vertex;
			 
			 vertexData.x += _Time.x * 5f;
			 
			 gl_Position = gl_ModelViewProjectionMatrix * vertexData;
			 
			}
			
			#endif
			
			#ifdef FRAGMENT
			
			void main()
			{
				gl_FragColor = texture2D(_MainTex, uv) * vertexColor;
			}
			
			#endif

			
			ENDGLSL
		}
	}

}