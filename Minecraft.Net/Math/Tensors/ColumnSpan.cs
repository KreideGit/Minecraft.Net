namespace Minecraft.Net.Math.Tensors;

public readonly ref struct ColumnSpan<T>
{
    public readonly int Length { get; }

    private readonly T[] _data;
    private readonly int _column;
    private readonly int _columns;
    
    public ColumnSpan(T[] data, int column, int rows, int columns, int layers)
    {
        Length = rows * layers;
        _data = data;
        _column = column;
        _columns = columns;
    }

    public ref T this[int index]
    {
        get => ref _data[_column + _columns * index];
    }
}