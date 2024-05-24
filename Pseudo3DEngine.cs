namespace MyEngine;

class Pseudo3DEngine : IEngine
{
    private void RayCast(int x)
    {
        // TODO
    }

    public bool UpdateFrame(int width, int height)
    {
        for (int i = 0; i < width; i++) {
            RayCast(i); 
        }
        return true;
    }
}
