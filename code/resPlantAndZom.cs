using System;
using Godot;
public enum PlantType
{
    peaShooter,
    rePeater
}
public enum ZomType
{
    zombie,
    luZhang
}
public partial class resPlantAndZom : Node2D
{
    public static resPlantAndZom Instance;
    [Export] public PackedScene peaShooter;
    [Export] public PackedScene rePeater;

    [Export] public PackedScene zombie;
    [Export] public PackedScene luZhang;
    public override void _Ready()
    {
        Instance = this;
    }
    public PackedScene matchPlant(PlantType plantType)
    {
        switch (plantType)
        {
            case PlantType.peaShooter:
                return peaShooter;
            case PlantType.rePeater:
                return rePeater;
            default:
                return null;
        }
    }

    public PackedScene matchZom(ZomType zomType)
    {
        switch (zomType)
        {
            case ZomType.zombie:
                return zombie;
            case ZomType.luZhang:
                return luZhang;
            default:
                return null;
        }
    }
}