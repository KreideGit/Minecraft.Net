﻿using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Minecraft.Net.Rendering;

public class Texture : IDisposable
{
    private readonly int _handle;

    public Texture(string path)
    {
        using var image = Image.Load<Rgba32>(path);
        image.Mutate(x => x.Flip(FlipMode.Vertical));

        var pixels = new List<byte>(4 * image.Width * image.Height);
        for (var y = 0; y < image.Height; y++)
        {
            var row = image.GetPixelRowSpan(y);
            for (var x = 0; x < image.Width; x++)
            {
                pixels.Add(row[x].R);
                pixels.Add(row[x].G);
                pixels.Add(row[x].B);
                pixels.Add(row[x].A);
            }
        }

        _handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _handle);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
    }

    public void Bind(TextureUnit slot = TextureUnit.Texture0)
    {
        GL.ActiveTexture(slot);
        GL.BindTexture(TextureTarget.Texture2D, _handle);
    }
    public void Unbind() => GL.BindTexture(TextureTarget.Texture2D, 0);
    public void Dispose() => GL.DeleteTexture(_handle);
}