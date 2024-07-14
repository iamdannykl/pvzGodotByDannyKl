using Godot;
using System;

public partial class reciever : Label
{
    public guanQiaType thisGuanQiaType;
    public int sunNum;
    public int RcHangShu;
    public int RcmapIndex;
    public void recieveData(guanQiaType guanQiaType, int sunOriginalNum, int mapIndex)
    {
        thisGuanQiaType = guanQiaType;
        sunNum = sunOriginalNum;
        RcmapIndex = mapIndex;
        if (RcmapIndex == 0 || RcmapIndex == 1 || RcmapIndex == 4)
        {
            RcHangShu = 5;
        }
        else if (RcmapIndex == 2 || RcmapIndex == 3)
        {
            RcHangShu = 6;
        }
        Text = "GQ:" + guanQiaType + "sun:" + sunOriginalNum;
    }
}
