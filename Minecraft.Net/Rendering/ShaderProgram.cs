using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minecraft.Net.Rendering;

public class ShaderProgram : IDisposable
{
    private readonly Dictionary<ShaderType, string> _shaders;
    private readonly List<int> _shaderHandles;
    private int _handle;

    public ShaderProgram()
    {
        _shaders = new();
        _shaderHandles = new();
        _handle = 0;
    }

    public string this[ShaderType type]
    {
        get => _shaders[type];
        init => _shaders[type] = value;
    }

    public ShaderProgram LoadSourceFiles()
    {
        foreach (var shader in _shaders)
        {
            var handle = GL.CreateShader(shader.Key);
            var content = File.ReadAllText(shader.Value);

            GL.ShaderSource(handle, content);
            _shaderHandles.Add(handle);
        }

        return this;
    }

    public ShaderProgram Compile()
    {
        foreach (var handle in _shaderHandles)
        {
            GL.CompileShader(handle);
            Console.Write(GL.GetShaderInfoLog(handle));
        }

        return this;
    }

    public ShaderProgram Link()
    {
        _handle = GL.CreateProgram();
        foreach (var handle in _shaderHandles)
        {
            GL.AttachShader(_handle, handle);
        }

        GL.LinkProgram(_handle);
        Console.Write(GL.GetProgramInfoLog(_handle));

        foreach (var handle in _shaderHandles)
        {
            GL.DetachShader(_handle, handle);
            GL.DeleteShader(handle);
        }

        _shaderHandles.Clear();
        return this;
    }

    public void UploadMat4(string name, Matrix4 matrix)
    {
        var location = GL.GetUniformLocation(_handle, name);
        GL.UniformMatrix4(location, false, ref matrix);
    }

    public void UploadVec3(string name, Vector3 vector)
    {
        var location = GL.GetUniformLocation(_handle, name);
        GL.Uniform3(location, ref vector);
    }

    public void Bind() => GL.UseProgram(_handle);
    public void Unbind() => GL.UseProgram(0);
    public void Dispose() => GL.DeleteProgram(_handle);
}