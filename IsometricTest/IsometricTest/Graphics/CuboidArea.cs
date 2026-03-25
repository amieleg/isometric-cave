using System.Security.Cryptography.X509Certificates;

public class CuboidArea
{
    public (int, int, int) _StartPoint;
    public (int, int, int) _EndPoint;

    public CuboidArea((int, int, int) StartPoint, (int, int, int) EndPoint)
    {
        _StartPoint = StartPoint;
        _EndPoint = EndPoint;
    }
}