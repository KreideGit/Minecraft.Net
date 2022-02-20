namespace Minecraft.Net.Math.Tensors;

public readonly ref struct RowSpan<T>
{
    public int Length { get; }

    private readonly T[] _data;
    private readonly int _row;
    private readonly int _rows;
    private readonly int _columns;

    public RowSpan(T[] data, int row, int rows, int columns, int layers)
    {
        Length = columns * layers;
        _data = data;
        _row = row;
        _rows = rows;
        _columns = columns;
    }

    public ref T this[int index]
    {
        get => ref _data[index * _rows -  index % _columns * (_rows - 1) + _row * _columns];
    }
}