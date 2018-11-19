#version 410
layout (location = 0) in vec2 position;

uniform mat3 transform;
uniform mat3 view;
uniform mat3 projection;

void main() 
{
    gl_Position = vec4(projection * view * transform * vec3(position, 0.0f), 1.0f);
}