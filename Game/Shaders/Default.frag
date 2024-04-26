#version 330 core

in vec2 texCoord;
in vec3 blockColor;
in vec4 skylight;

out vec4 FragColor;

uniform sampler2D texture0;

void main() 
{
vec4 color = texture(texture0, texCoord);


    //vec4 a_color.r = blockColor.r;
    //a_color.g = blockColor.g;
    //a_color.b = blockColor.b;
    //a_color.a = 1.0;
    vec3 rgb = color.rgb+skylight.r;
    color = vec4(rgb,color.a);

    color = color*vec4(blockColor,1);

    


    
    FragColor = color;

	//FragColor  = texture(texture0, texCoord);
	
	
}
