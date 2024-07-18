using System;
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

    public hang(int hangNum, Vector2 hangLeftTou, HangType hangLeiXing)
    {
        hangShu = hangNum;
        hangTou = hangLeftTou;
        hangType = hangLeiXing;
    }
}