using System.Numerics;
using System;

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

    public float Length()
    {
        return Math.Abs((Point2 - Point1).Length());
    }

    public Vector2? IntersectsWith(Segment segment) // Returns the point where two segments cross or null if they don't or if they are equal
    {
        // If one of the segments is vertical, have to use other method, otherwise will be dividion by zero
        if (Point1.X == Point2.X || segment.Point1.X == segment.Point2.X) 
        {
            bool segment1vert = Point1.X == Point2.X;
            bool segment2vert = segment.Point1.X == segment.Point2.X;

            if (segment1vert && segment2vert) return null; // If so they either don't cross or are equal
            Segment NonVertical = segment1vert ? this : segment;
            Segment Vertical = segment2vert ? this : segment;

            float expr = (NonVertical.Point2.Y - NonVertical.Point1.Y) / (NonVertical.Point2.X - NonVertical.Point1.X);
            
            float crossX = Vertical.Point1.X;
            float crossY = NonVertical.Point1.Y + crossX * expr - NonVertical.Point1.X * expr;

            return new Vector2(crossX, crossY);
        }

        // Danger: complicated math area
        float a1x = this.Point1.X;
        float a1y = this.Point1.Y;

        float b1x = this.Point2.X;
        float b1y = this.Point2.Y;

        float a1x = segment.Point1.X;
        float a1y = segment.Point1.Y;

        float b1x = segment.Point2.X;
        float b1y = segment.Point2.Y;

        float e1 = (b1y - a1y) / (b1x - a1x);
        float e2 = (b2y - a2y) / (b2x - a2x);

        if (e2 - e1 == 0) return null; 

        float crossY = (a1y * e2 - a2y * e1 + a2x * e2 * e1 - a1x * e1 * e2) / (e2 - e1);
        float crossX = -((a1y - a1x * e1 - crossY) / e1);

        return new Vector2(crossX, crossY);
    }
}
