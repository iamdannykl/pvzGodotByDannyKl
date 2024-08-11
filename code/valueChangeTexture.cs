using Godot;
using System;

public partial class valueChangeTexture : TextureProgressBar
{
    public void changeIt(float value)
    {
        Value = value;
    }
}
