using Minecraft.Net.Rendering;
using OpenTK.Graphics.OpenGL4;

namespace Minecraft.Net.Terrain;

public class World
{
    private readonly List<Chunk> _loadedChunks;
    private readonly ShaderProgram _chunkShader;

    public World()
    {
        _loadedChunks = new();
        _chunkShader = new()
        {
            [ShaderType.VertexShader] = @"Resources/Shaders/ChunkVertexShader.glsl",
            [ShaderType.FragmentShader] = @"Resources/Shaders/ChunkFragmentShader.glsl"
        };

        _chunkShader.LoadSourceFiles().Compile().Link();

        _loadedChunks.Add(new());
    }

    public void Update(double deltaTime)
    {
        for(var i = 0; i < _loadedChunks.Count; i++)
        {
            _loadedChunks[i].Update(deltaTime);
        }
    }

    public void Render()
    {
        _chunkShader.Bind();
        for (var i = 0; i < _loadedChunks.Count; i++)
        {
            _loadedChunks[i].Render();
        }
    }
}