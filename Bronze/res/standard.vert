#version 410
layout (location = 0) in vec2 position;
layout (location = 1) in vec2 texCoord;

uniform mat3 transform;

out vec2 TexCoord;

void main() 
{
    gl_Position = vec4(transform * vec3(position, 1.0f), 1.0f);
    TexCoord = texCoord;
}