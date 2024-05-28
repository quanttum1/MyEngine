using System.Numerics;
using System.Math;

namespace MyEngine;

class Segment
{
    public Vector2 Point1;
    public Vector2 Point2;

    public Segment(Vector2 p1, Vector2 p2)
    {
        Point1 = p1;
        Point2 = p2;
    }

    public Segment(float p1x, float p1y, float p2x, float p2y)
    {
        Point1 = new Vector2(p1x, p1y);
        Point2 = new Vector2(p2x, p2y);
    }

    public Length()
    {
        return Abs((Point2 - Point1).Length());
    }
}
