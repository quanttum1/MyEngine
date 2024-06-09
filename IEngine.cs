using SFML.Graphics;

namespace MyEngine;

// TODO: Maybe make it an abstract class to enforce classes that implement it to have specific constructtor signature
interface IEngine
{
    public bool UpdateFrame(); // Return true if the game should be continued
}
