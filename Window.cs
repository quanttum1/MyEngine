using SFML.Window;
using SFML.Graphics;
using System.Numerics;

namespace MyEngine;

public class Window
{
    private const string Title = "My Engine";
    private RenderWindow _window;
    private Vector2 _resolution;

    public Window()
    {
        VideoMode mode = VideoMode.FullscreenModes[0];

        _resolution.X = mode.Width;
        _resolution.Y = mode.Height;

        _window = new RenderWindow(mode, Title, Styles.Fullscreen);
        _window.SetVerticalSyncEnabled(true);

        _window.Closed += (sender, args) => _window.Close();
    }

    public void Run() {
        _window.KeyPressed += KeyPressedHandler;
        IEngine engine = new Minimap(_window, _window);

        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            _window.Clear(Color.Black);

            if (!engine.UpdateFrame())
            {
                _window.Close();
            }

            _window.Display();
        }
    }

    private void KeyPressedHandler(object? sender, KeyEventArgs e)
    {
        switch (e.Code)
        {
            case Keyboard.Key.Q:
                _window.Close();
                break;
        }
    }
}

