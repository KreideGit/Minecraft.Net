using Minecraft.Net.Math.Tensors;
using Minecraft.Net.Rendering;
using System.Diagnostics;

namespace Minecraft.Net.Terrain;

public class Chunk : Tensor3<Block>
{
    private readonly ChunkMesh _mesh;

    public Chunk()
        : base(16, 16, 256)
    {
        var texture = new Texture(@"Resources/Textures/bricks.jpg");
        _mesh = new();
        _mesh.AddTriangle();
        _mesh.Flush();
    }

    public void Update(double deltaTime)
    {
        for(var i = 0; i < TotalElements; i++)
        {
            //this[i].Update(deltaTime);
        }
    }

    public void Render()
    {
        _mesh.Render();
    }
}