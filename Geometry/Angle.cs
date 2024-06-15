using System;
using System.Numerics;

namespace MyEngine.Geometry;

public class Angle
{
    public double Radians = 0;
    public double Degrees
    {
        get
        {
            return Radians * (180.0 / Math.PI);
        }
        set
        {
            Radians = value * (Math.PI / 180.0);
        }
    }

    private Angle() {}

    static public Angle FromDegrees(double degrees)
    {
        Angle angle = new Angle();
        angle.Degrees = degrees;
        return angle;
    }

    static public Angle FromRadians(double radians)
    {
        Angle angle = new Angle();
        angle.Radians = radians;
        return angle;
    }

    public Vector2 ToVector() {
        return new Vector2((float)Math.Cos(Radians), (float)Math.Sin(Radians));
    }
}
