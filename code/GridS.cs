using System;
using Godot;

public partial class GridS
{
    public Vector2 Point;
    //世界坐标
    public Vector2 Position;
    //是否有植物
    public bool Plant;
    public bool Zombie;
    public int Num;
    public GridS(Vector2 point, Vector2 position, bool plant, bool zombie, int num)
    {
        Point = point;
        Position = position;
        Plant = plant;
        Zombie = zombie;
        Num = num;
    }
}
