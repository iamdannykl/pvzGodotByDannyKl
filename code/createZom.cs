using Godot;
using System;
using System.Collections.Generic;

public partial class createZom : Sprite2D
{
    public saveContent loadData;
    [Export] public Timer waveTimer;
    public bool isUPDT;
    public int allZomNum, allCrtZom;
    public int waveNow = -1;
    public float timePerWave;
    private float nowTime;
    GridSys gridSys;
    public override void _Ready()
    {
        base._Ready();
        gridSys = GetNode<GridSys>("GridSys");
    }
    public void _on_timer_timeout()
    {
        if (reciever.Instance.playMode == playMode.player)
        {
            isUPDT = true;
            loadData = reciever.Instance.gqData;
            GD.Print("waveNow:" + waveNow);
            GD.Print("zong:" + loadData.waves.Count);
        }
        else
        {
            isUPDT = false;
        }
        if (isUPDT)
        {
            if (waveNow < loadData.waves.Count - 1)
            {
                ifChaoShi();
            }
        }
    }
    public void jiShiJieShu()
    {
        if (isUPDT)
        {
            if (waveNow < loadData.waves.Count - 1)
            {
                ifChaoShi();
            }
        }
    }
    public override void _PhysicsProcess(double delta)
    {

    }
    void CRTzombies(int num, int zrNum)
    {
        for (int i = 0; i < num; i++)
        {
            GD.Print("dangQian:wave zr zom" + waveNow + "/" + zrNum + "/" + i);
            GD.Print("zomtypeInt:" + loadData.waves[waveNow].zrs[zrNum].zomInfos[i].zomType);
            zomInfo zif = loadData.waves[waveNow].zrs[zrNum].zomInfos[i];
            GD.Print(gridSys);
            GD.Print(gridSys.hangList);
            GD.Print(gridSys.hangList.Count);
            GD.Print("zif:" + (zif.hangShu - 1));
            GD.Print(gridSys.hangList[zif.hangShu - 1].zomInfos);
            gridSys.hangList[zif.hangShu - 1].zomInfos.Add(zif);
            /* zombie_base zom = resPlantAndZom.Instance.matchZom(zif.zomType).Instantiate<zombie_base>();
            zom.GlobalPosition = loadData.waves[waveNow].zrs[zrNum].zomInfos[i].pos + new Vector2(GridSys.Instance.XjianGe * 10, 0);
            zom.crtIt();
            GetTree().CurrentScene.AddChild(zom); */
            allCrtZom += 1;
        }
        gridSys.LuanXuHang();
    }
    void ifChaoShi()
    {
        int shangBoCrtLeft = allCrtZom - allZomNum;
        allZomNum = 0;
        waveNow++;
        waveTimer.Start();
        nextBo();
        allCrtZom += shangBoCrtLeft;
    }
    void nextZr(int zrnum, int jishu)
    {
        int Znum = loadData.waves[waveNow].zrs[zrnum].zomInfos.Count;
        int jis = jishu;
        if (Znum > 0)
        {
            CRTzombies(Znum, zrnum);
        }
        else
        {
            if (zrnum < 2)
            {
                zrnum++;
            }
            else
            {
                zrnum = 0;
            }
            jis++;
            if (jis < 3)
            {
                nextZr(zrnum, jis);
            }
            else
            {
                GD.Print("ERROR EACH ZR IS NULL!");
            }
        }
    }
    public void nextBo()
    {
        GD.Print("nextBo");
        waveTimer.Start();
        allCrtZom = 0;
        allZomNum = 0;
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();
        int suiJi = random.RandiRange(1, 3);
        int jiShu = 0;
        nextZr(suiJi - 1, jiShu);
    }
}
