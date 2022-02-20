namespace Minecraft.Net.Math.Tensors;

public class Tensor2<T>
{
    public int Rows { get; }
    public int Columns { get; }

    private readonly T[] _data;

    public Tensor2(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        _data = new T[Rows * Columns];
    }

    public T this[int row, int column]
    {
        get => _data[column + Columns * row];
        set => _data[column + Columns * row] = value;
    }

    public Span<T> AsSpan() => new Span<T>(_data);
    public RowSpan<T> AsRowSpan(int row) => new RowSpan<T>(_data, row, Rows, Columns, 1);
    public ColumnSpan<T> AsColumnSpan(int column) => new ColumnSpan<T>(_data, column, Rows, Columns, 1);
}