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
    snowPeaShooter,
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
    pea,
    snowPea
}
public partial class resPlantAndZom : Node2D
{
    public Dictionary<PlantType, PackedScene> plantTypeToPackedScene;
    public Dictionary<BulletType, PackedScene> bulletTypeToPackedScene;
    public static resPlantAndZom Instance;
    [Export] public PackedScene peaShooter;
    [Export] public PackedScene rePeater;
    [Export] public PackedScene sunFlower;
    [Export] public PackedScene potatoDL;
    [Export] public PackedScene nut;
    [Export] public PackedScene cherryBaoDan;
    [Export] public PackedScene snowPeaShooter;
    [Export] public PackedScene heYe;
    //Zombies===============================================================
    [Export] public PackedScene zombie;
    [Export] public PackedScene luZhang;
    //Bullets===============================================================
    [Export] public PackedScene pea;
    [Export] public PackedScene snowPea;
    public override void _Ready()
    {
        Instance = this;
        plantTypeToPackedScene = new Dictionary<PlantType, PackedScene>{
        {PlantType.peaShooter,peaShooter},
        {PlantType.sunFlower,sunFlower},
        {PlantType.potatoDL,potatoDL},
        {PlantType.cherryBaoDan,cherryBaoDan},
        {PlantType.snowPeaShooter,snowPeaShooter},
        {PlantType.nut,nut},
        {PlantType.rePeater,rePeater},
        {PlantType.heYe,heYe}
    };
        bulletTypeToPackedScene = new Dictionary<BulletType, PackedScene>{
        {BulletType.pea,pea},
        {BulletType.snowPea,snowPea}
    };
    }
    public PackedScene matchPlant(PlantType plantType)
    {
        if (plantTypeToPackedScene.TryGetValue(plantType, out PackedScene scene))
        {
            return scene;
        }
        return null;
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
        if (bulletTypeToPackedScene.TryGetValue(bulletType, out PackedScene scene))
        {
            return scene;
        }
        return null;
    }
}