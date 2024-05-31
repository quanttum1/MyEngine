using System.Numerics;
using System;

namespace MyEngine;

class Pseudo3DEngine : IEngine
{
    private Vector2 _position = new Vector2(0, 0); // Camera position
    private float _turnAngle = 0; // Camera's turn angle in degrees
    private const float ViewAngle = 90; // Specifies how "many degrees" you can see
    private const float RayLength = 100; // Specifies how far you can see

    private void RayCast(int x, int width)
    {
        double rayAngle = _turnAngle; // Raw camera's turn angle 
        rayAngle += ViewAngle * x / width; // Shifts the angle specificly for this ray
        rayAngle -= ViewAngle / 2; // Shifts back for a half of ViewAngle to make camera's turn at the middle of camera view
        rayAngle *= Math.PI / 180; // Converts to radians

        Vector2 rayDirection = new Vector2(Math.Cos(rayAngle), Math.Sin(rayAngle)); // Converts to vector
        rayDirection *= RayLength; // Sets length of ray

        Segment ray = new Segment(_position, _position + rayDirection); // Converts to segment

        Segment wall = new Segment(-2, 5, 2, 5); // Test wall

        
    }

    public bool UpdateFrame(int width, int height)
    {
        for (int i = 0; i < width; i++) { // Goes trough all "colums" of the window
            RayCast(i, width); 
        }
        return true;
    }
}
