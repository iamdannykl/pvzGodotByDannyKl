using System;
using System.Collections.Generic;
using Godot;
public enum PlantType
{
    peaShooter,
    rePeater,
    sunFlower,
    potatoDL,
    cherryBaoDan,
    nut,
    heYe
}
public enum ZomType
{
    zombie,
    luZhang
}
public enum BulletType
{
    pea
}
public partial class resPlantAndZom : Node2D
{
    public Dictionary<PlantType, PackedScene> plantTypeToPackedScene;
    public static resPlantAndZom Instance;
    [Export] public PackedScene peaShooter;
    [Export] public PackedScene rePeater;
    [Export] public PackedScene sunFlower;
    [Export] public PackedScene potatoDL;
    [Export] public PackedScene nut;
    [Export] public PackedScene cherryBaoDan;
    [Export] public PackedScene heYe;
    //Zombies===============================================================
    [Export] public PackedScene zombie;
    [Export] public PackedScene luZhang;
    //Bullets===============================================================
    [Export] public PackedScene pea;
    public override void _Ready()
    {
        Instance = this;
        plantTypeToPackedScene = new Dictionary<PlantType, PackedScene>{
        {PlantType.peaShooter,peaShooter},
        {PlantType.sunFlower,sunFlower},
        {PlantType.potatoDL,potatoDL},
        {PlantType.cherryBaoDan,cherryBaoDan},
        {PlantType.nut,nut},
        {PlantType.rePeater,rePeater},
        {PlantType.heYe,heYe}
    };
    }
    public PackedScene matchPlant(PlantType plantType)
    {
        if (plantTypeToPackedScene.TryGetValue(plantType, out PackedScene scene))
        {
            return scene;
        }
        return null;
        /* switch (plantType)
        {
            case PlantType.peaShooter:
                return peaShooter;
            case PlantType.rePeater:
                return rePeater;
            case PlantType.sunFlower:
                return sunFlower;
            case PlantType.potatoDL:
                return potatoDL;
            case PlantType.cherryBaoDan:
                return cherryBaoDan;
            case PlantType.heYe:
                return heYe;
            default:
                return null;
        } */
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
    public PackedScene matchBullet(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.pea:
                return pea;
            default:
                return null;
        }
    }
}