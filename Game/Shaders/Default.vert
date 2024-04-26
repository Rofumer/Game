#version 330 core
layout (location = 0) in vec3 aPosition; // vertex coordinates
layout (location = 1) in vec2 aTexCoord; // texture coordinates
layout (location = 2) in vec3 aBlockColor; // texture coordinates

out vec2 texCoord;
out vec3 blockColor;
out vec4 skylight;

// uniform variables
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec4 light;

void main() 
{
	gl_Position = vec4(aPosition, 1.0) * model * view * projection; // coordinates
	texCoord = aTexCoord;
	blockColor = aBlockColor;
	skylight=light;
}
