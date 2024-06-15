using System.Numerics;
using System;

using SFML.System;

namespace MyEngine.Geometry;

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

    public float Length()
    {
        return Math.Abs((Point2 - Point1).Length());
    }

    public Vector2? IntersectsWith(Segment segment) // Returns the point where two segments cross or null if they don't or if they are equal
    {
        double crossX = 0, crossY = 0;
        // If one of the segments is vertical, have to use other method, otherwise will be dividion by zero
        if (Point1.X == Point2.X || segment.Point1.X == segment.Point2.X) 
        {
            bool segment1vert = Point1.X == Point2.X;
            bool segment2vert = segment.Point1.X == segment.Point2.X;

            if (segment1vert && segment2vert) return null; // If so they either don't cross or are equal
            Segment NonVertical = segment1vert ? this : segment;
            Segment Vertical = segment2vert ? this : segment;

            double expr = (NonVertical.Point2.Y - NonVertical.Point1.Y) / (NonVertical.Point2.X - NonVertical.Point1.X);
            
            crossX = Vertical.Point1.X;
            crossY = NonVertical.Point1.Y + crossX * expr - NonVertical.Point1.X * expr;
        } else {
            double a1x = this.Point1.X;
            double a1y = this.Point1.Y;

            double b1x = this.Point2.X;
            double b1y = this.Point2.Y;

            double a2x = segment.Point1.X;
            double a2y = segment.Point1.Y;

            double b2x = segment.Point2.X;
            double b2y = segment.Point2.Y;

            double e1 = (b1y - a1y) / (b1x - a1x);
            double e2 = (b2y - a2y) / (b2x - a2x);

            if (e2 - e1 == 0) return null; 

            crossY = (a1y * e2 - a2y * e1 + a2x * e2 * e1 - a1x * e1 * e2) / (e2 - e1);
            crossX = -((a1y - a1x * e1 - crossY) / e1);
        }

        Vector2 crossPoint = new Vector2((float)crossX, (float)crossY);
        if (IsPointOnSegment(crossX, crossY) && segment.IsPointOnSegment(crossX, crossY)) return crossPoint;
        return null;
    }

    // Does NOT really check whether point is on the LINE
    // If you use this function consider that you give a point that lies on the line you get if you extend the segment
    public bool IsPointOnSegment(double x, double y) {
        Vector2 leftPoint = Point1.X >= Point2.X ? Point1 : Point2;
        Vector2 rightPoint = Point1.X <= Point2.X ? Point1 : Point2;
        Vector2 topPoint = Point1.Y <= Point2.Y ? Point1 : Point2;
        Vector2 bottomPoint = Point1.Y >= Point2.Y ? Point1 : Point2;

        if ((x <= leftPoint.X || _isOproximatlyEqual(x, leftPoint.X)) &&
            (x >= rightPoint.X || _isOproximatlyEqual(x, rightPoint.X)) &&
            (y >= topPoint.Y || _isOproximatlyEqual(y, topPoint.Y)) &&
            (y <= bottomPoint.Y || _isOproximatlyEqual(y, bottomPoint.Y)))
            return true;
        return false;
    }

    private bool _isOproximatlyEqual(double a, double b)
    {
        return Math.Abs(a - b) < 0.000001;
    }
}
