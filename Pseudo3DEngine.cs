using System.Numerics;
using System;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

using MyEngine.Geometry;

namespace MyEngine;

class Pseudo3DEngine : IEngine
{
    public Pseudo3DEngine(RenderTarget target, RenderWindow window) // window is given to add events
    {
        _target = target;
        _controls = new Controls(window);
    }

    public bool UpdateFrame()
    {
        int width = (int)_target.Size.X;
        int height = (int)_target.Size.Y;

        for (int i = 0; i < width; i++) { // Goes trough all "colums" of the window
            RayCast(i, width, height);
        }
        return true;
    }

    private void RayCast(int x, int width, int height)
    {
        double rayAngle = _controls.TurnAngle; // Raw camera's turn angle 
        rayAngle += ViewAngle * x / width; // Shifts the angle specificly for this ray
        rayAngle -= ViewAngle / 2; // Shifts back for a half of ViewAngle to make camera's turn at the middle of camera view
        rayAngle *= Math.PI / 180; // Converts to radians

        Vector2 rayDirection = Angle.FromDegrees(rayAngle).ToVector();
        rayDirection *= RayLength; // Sets length of ray

        Segment ray = new Segment(_controls.Position, _controls.Position + rayDirection); // Converts to segment

        Segment wall = new Segment(-2, 5, 2, 5); // Test wall

        float? distance = (ray.IntersectsWith(wall) - _controls.Position)?.Length();

        if (distance == null || distance == 0.0) return;

        float lineSize = height / (float)distance;
        
        var line = new RectangleShape();
        line.Position = new Vector2f((float)x, height / 2 - lineSize / 2);
        line.FillColor = Color.White;
        line.Size = new Vector2f(1, lineSize);
        _target.Draw(line);
    }

    private const float ViewAngle = 90; // Specifies how "many degrees" you can see
    private const float RayLength = 100; // Specifies how far you can see
    private RenderTarget _target;
    private Controls _controls;
}
