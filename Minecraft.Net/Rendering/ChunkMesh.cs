using Minecraft.Net.Utilities;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minecraft.Net.Rendering;

public struct BlockVertex
{
    public Vector3 Position { get; set; }
    public Vector2 TexCoord { get; set; }
}

public class ChunkMesh
{
    private int _vertexBufferHandle;
    private int _indexBufferHandle;
    private int _vertexArrayHandle;
    private int _indexCount;
    private readonly List<BlockVertex> _vertices;
    private readonly List<uint> _indices;

    public ChunkMesh()
    {
        _vertices = new();
        _indices = new();
    }

    public void AddTriangle()
    {
        _vertices.Add(new BlockVertex() { Position = new(-0.5f, -0.5f, 0.0f), TexCoord = new Vector2(0.0f, 0.0f) });
        _vertices.Add(new BlockVertex() { Position = new( 0.5f, -0.5f, 0.0f), TexCoord = new Vector2(1.0f, 0.0f) });
        _vertices.Add(new BlockVertex() { Position = new( 0.0f,  0.5f, 0.0f), TexCoord = new Vector2(0.5f, 0.5f) });
        _indices.Add(0);
        _indices.Add(1);
        _indices.Add(2);
    }

    public void AddFace()
    {

    }

    public void Flush()
    {
        _indexCount = _indices.Count;

        var vertexSize = 5 * sizeof(float);//Marshal.SizeOf(typeof(Vertex));
        var indexSize = sizeof(uint);

        _indexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _indexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, _indices.Count * indexSize, _indices.GetInternalArray(), BufferUsageHint.StaticDraw);
       
        _vertexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * vertexSize, _vertices.GetInternalArray(), BufferUsageHint.StaticDraw);

        _vertexArrayHandle = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayHandle);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, vertexSize, 0);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, vertexSize, 3 * sizeof(float));

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferHandle);

        _vertices.Clear();
        _indices.Clear();
    }

    public void Reset()
    {
        GL.DeleteVertexArray(_vertexArrayHandle);
        GL.DeleteBuffer(_vertexBufferHandle);
        GL.DeleteBuffer(_indexBufferHandle);
    }

    public void Render()
    {
        GL.BindVertexArray(_vertexArrayHandle);
        GL.DrawElements(PrimitiveType.Triangles, _indexCount, DrawElementsType.UnsignedInt, 0);
    }
}