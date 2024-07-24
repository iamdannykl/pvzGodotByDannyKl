using Godot;
using System;

public partial class createZom : Sprite2D
{
    public saveContent loadData;
    [Export] public Timer waveTimer;
    public bool isUPDT;
    public int allZomNum, allCrtZom;
    public int waveNow = -1;
    public float timePerWave;
    private float nowTime;
    public override void _Ready()
    {
        base._Ready();
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
            zombie_base zom = resPlantAndZom.Instance.matchZom(loadData.waves[waveNow].zrs[zrNum].zomInfos[i].zomType).Instantiate<zombie_base>();
            zom.GlobalPosition = loadData.waves[waveNow].zrs[zrNum].zomInfos[i].pos;
            GetTree().CurrentScene.AddChild(zom);
            allCrtZom += 1;
        }
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
    public void nextBo()
    {
        GD.Print("nextBo");
        waveTimer.Start();
        allCrtZom = 0;
        allZomNum = 0;
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();
        int suiJi = random.RandiRange(1, 3);
        int geShu = 0;
        int Znum = 0;
        switch (suiJi)
        {
            case 1:
                geShu = loadData.waves[waveNow].zrs[0].zomInfos.Count;
                GD.Print("zr1" + geShu);
                Znum = loadData.waves[waveNow].zrs[0].zomInfos.Count;

                CRTzombies(Znum, 0);

                break;
            case 2:
                geShu = loadData.waves[waveNow].zrs[1].zomInfos.Count;
                GD.Print("zr2" + geShu);

                Znum = loadData.waves[waveNow].zrs[1].zomInfos.Count;

                CRTzombies(Znum, 1);

                break;
            case 3:
                geShu = loadData.waves[waveNow].zrs[2].zomInfos.Count;
                GD.Print("zr3" + geShu);

                Znum = loadData.waves[waveNow].zrs[2].zomInfos.Count;

                CRTzombies(Znum, 2);

                break;
            default:
                Znum = -1;
                geShu = -1;
                break;
        }
    }
}
