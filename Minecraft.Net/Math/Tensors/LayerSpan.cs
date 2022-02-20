namespace Minecraft.Net.Math.Tensors;

public readonly ref struct LayerSpan<T>
{
    public readonly int Length { get; }

    private readonly T[] _data;
    private readonly int _layer;
    private readonly int _stride;

    public LayerSpan(T[] data, int layer, int stride)
    {
        Length = stride;
        _data = data;
        _layer = layer;
        _stride = stride;
    }

    public ref T this[int index]
    {
        get => ref _data[index + _layer * _stride];
    }
}