using System.Numerics;
using System.Math;

namespace MyEngine;

class Pseudo3DEngine : IEngine
{
    private Vector2 _position = new Vector2(0, 0);
    private float _turnAngle = 0; // Camera's turn angle in degrees
    private const float ViewAngle = 90;
    private const float RayLength = 100;

    private void RayCast(int x, int width)
    {
        double rayAngle = _turnAngle; // Raw camera's turn angle 
        rayAngle += ViewAngle * x / width; // Shifts the angle specificly for this ray
        rayAngle -= ViewAngle / 2; // Shifts back for a half of ViewAngle to make camera's turn at the middle of camera view
        rayAngle *= PI / 180; // Converts to radians

        Vector2 rayDirection = new Vector2(Cos(rayAngle), Sin(rayAngle));
        rayDirection *= RayLength;

        Segment ray = new Segment(_position, _position + rayDirection);

        // TODO
    }

    public bool UpdateFrame(int width, int height)
    {
        for (int i = 0; i < width; i++) {
            RayCast(i, width); 
        }
        return true;
    }
}
