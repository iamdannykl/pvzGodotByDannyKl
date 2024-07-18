using System.Numerics;
using Godot;
using Vector2 = Godot.Vector2;
public class zomInfo
{
    public Vector2 pos;
    public ZomType zomType;
    public zomInfo(Vector2 wz, ZomType ztp)
    {
        pos = wz;
        zomType = ztp;
    }
}