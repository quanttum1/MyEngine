using System.Numerics;

using SFML.Window;
using SFML.Graphics;

using MyEngine.Geometry;

namespace MyEngine;

public class Controls
{
    public Vector2 Position = new Vector2(0, 0); // Camera position
    public float TurnAngle = 100; // Camera's turn angle in degrees

    public Controls(RenderWindow window)
    {
        window.KeyPressed += (object? sender, KeyEventArgs e) => {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    Position += Angle.FromDegrees((float)(TurnAngle * (Math.PI / 180))).ToVector();
                    break;
                case Keyboard.Key.S:
                    Position -= Angle.FromDegrees((float)(TurnAngle * (Math.PI / 180))).ToVector();
                    break;
                case Keyboard.Key.A:
                    TurnAngle -= 5;
                    break;
                case Keyboard.Key.D:
                    TurnAngle += 5;
                    break;
            }
        };

        window.KeyReleased += (object? sender, KeyEventArgs e) => {};
    }
}
