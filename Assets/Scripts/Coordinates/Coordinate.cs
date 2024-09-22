public struct Coordinate
{
    private int _x; 
    private int _y;

    public Coordinate(int x, int y)
    { 
        _x = x;
        _y = y;
    }

    public int X => _x;
    public int Y => _y;
}
