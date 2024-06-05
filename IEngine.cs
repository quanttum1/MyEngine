using SFML.Graphics;

namespace MyEngine;

interface IEngine
{
    public bool UpdateFrame(); // Return true if the game should be continued
}
