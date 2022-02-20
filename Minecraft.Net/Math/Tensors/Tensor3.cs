namespace Minecraft.Net.Math.Tensors;

public class Tensor3<T>
{
    public int Rows { get; }
    public int Columns { get; }
    public int Layers { get; }
    public int TotalElements { get; }

    private readonly T[] _data;
    private readonly int _stride;

    public Tensor3(int rows, int columns, int layers)
    {
        Rows = rows;
        Columns = columns;
        Layers = layers;
        TotalElements = Rows * Columns * Layers;

        _data = new T[TotalElements];
        _stride = Rows * Columns;
    }

    public T this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    public T this[int row, int column, int layer]
    {
        get => _data[column + Columns * row + _stride * layer];
        set => _data[column + Columns * row + _stride * layer] = value;
    }

    public Span<T> AsSpan() => new Span<T>(_data);
    public RowSpan<T> AsRowSpan(int row) => new RowSpan<T>(_data, row, Rows, Columns, Layers);
    public ColumnSpan<T> AsColumnSpan(int column) => new ColumnSpan<T>(_data, column, Rows, Columns, Layers);
    public LayerSpan<T> AsLayerSpan(int layer) => new LayerSpan<T>(_data, layer, _stride);
}