using System.Numerics;

using SFML.Graphics;
using SFML.System;

namespace MyEngine;

public class Minimap : IEngine
{
    public Minimap(RenderTarget target, RenderWindow window)
    {
        _target = target;
        // TODO: controls
    }

    public bool UpdateFrame()
    {
        for (int i = 0; i < 20; i++)
        {
            RayCast(i, 20);
        }

        return true;
    }

    private void RayCast(int index, int total)
    {
        double rayAngle = _turnAngle; // Raw camera's turn angle 
        rayAngle += ViewAngle * index / total; // Shifts the angle specificly for this ray
        rayAngle -= ViewAngle / 2; // Shifts back for a half of ViewAngle to make camera's turn at the middle of camera view
        rayAngle *= Math.PI / 180; // Converts to radians

        Vector2 rayDirection = new Vector2((float)Math.Cos(rayAngle), (float)Math.Sin(rayAngle)); // Converts to vector
        rayDirection *= RayLength; // Sets length of ray

        Segment ray = new Segment(_position, _position + rayDirection); // Converts to segment
        Segment wall = new Segment(-50, 125, 500, 125); // Test wall

        float? distance = (ray.IntersectsWith(wall) - _position)?.Length();

        if (distance == null || distance == 0.0) return;

        DrawSegment(wall);

        DrawSegment(ray);
    }

    private void DrawSegment(Segment segment)
    {
        VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
        line[0] = new Vertex(new Vector2f(segment.Point1.X + 500, segment.Point1.Y + 500), Color.White);
        line[1] = new Vertex(new Vector2f(segment.Point2.X + 500, segment.Point2.Y + 500), Color.White);
        _target.Draw(line);
    }

    private Vector2 _position = new Vector2(0, 0); // Camera position
    private float _turnAngle = 100; // Camera's turn angle in degrees
    private const float ViewAngle = 90; // Specifies how "many degrees" you can see
    private const float RayLength = 300; // Specifies how far you can see
    RenderTarget _target;
}
