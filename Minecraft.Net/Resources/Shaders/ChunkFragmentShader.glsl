#version 460 core
out vec4 FragColor;
  
in vec2 o_tex;
uniform sampler2D sampler;
  
void main()
{
    FragColor = texture(sampler, o_tex);
}