using System;
using System.Collections.Generic;
using Godot;
public enum HangType
{
    tu,
    grass,
    water,
    roof
}
public partial class hang
{
    public int hangShu;
    public Vector2 hangTou;
    public HangType hangType;
    public List<zomInfo> zomInfos = new List<zomInfo>();
    public List<zomInfo> NewZomInfos = new List<zomInfo>();
    public void CRTzombies()
    {

    }

    public hang(int hangNum, Vector2 hangLeftTou, HangType hangLeiXing)
    {
        hangShu = hangNum;
        hangTou = hangLeftTou;
        hangType = hangLeiXing;
    }
}