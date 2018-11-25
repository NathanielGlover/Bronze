#version 410
in vec2 TexCoord;

out vec4 FragColor;

uniform sampler2D tex;

void main() {
    FragColor = texture(tex, vec2(TexCoord.x, -TexCoord.y));
}