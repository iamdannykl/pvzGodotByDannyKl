using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
public partial class GuanQiaSaver : Control
{
    public int ZRnum = -1;
    [Export] public Label waveNum;
    [Export] public Button nextButton;
    [Export] public Button finishButton;
    [Export] public Node2D zr1, zr2, zr3;
    public int boShu = 1;
    public List<wave> waves = new List<wave>();

    public override void _Ready()
    {
        waveNum.Text = "第" + boShu + "波";
        finishButton.Connect("button_up", new Callable(this, nameof(FinishSave)));
        waves.Add(new wave());
    }
    void FinishSave()
    {
        saveContent save = new saveContent(waves);
        GD.Print(save.waves.Count);
        string jsonString;
        JsonSerializerSettings setting = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            //Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };
        GD.Print("zrsshu:" + save.waves[0].zrs.Count);
        GD.Print("num:" + save.waves[0].zrs[0].zomInfos.Count);
        jsonString = JsonConvert.SerializeObject(save, setting);
        GD.Print(jsonString);
        if (File.Exists("D:\\fileSave\\gq.json"))
        {
            File.Delete("D:\\fileSave\\gq.json");
        }
        //FileStream fileStream = new FileStream("D:\\fileSave\\gq.json", FileMode.Create);
        StreamWriter sw = new StreamWriter("D:\\fileSave\\gq.json");
        sw.Write(jsonString);
        sw.Close();
    }
    public void deSerializeJsonToObject()
    {
        StreamReader sr = new StreamReader("D:\\fileSave\\gq.json");
        string jsonString = sr.ReadToEnd();
        sr.Close();
        GD.Print(jsonString);
        JsonSerializerSettings setting = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            //Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };
        saveContent gqContent = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
        GD.Print(gqContent.waves[0].zrs[0].zomInfos[0].pos);
        int i = 1;
        int j = 0;
        foreach (var WAVE in gqContent.waves)
        {
            j++;
            foreach (zr ZRSHU in WAVE.zrs)
            {
                GD.Print("zrs:" + i++);
            }
        }
        GD.Print("wave:" + j);
    }
    void clickNextButton()
    {
        /* foreach(var node in waves[boShu-1].zrs){
            
        } */
        waves.Add(new wave());
        if (ZRnum != -1)
        {
            waveNum.Text = "第" + (++boShu) + "波";
        }
        foreach (zombie_base node in zr1.GetChildren())
        {
            waves[boShu - 2].zrs[0].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType));
            //GD.Print(waves[boShu - 2].zrs[0].zomInfos.Count());
            node.QueueFree();
        }
        foreach (zombie_base node in zr2.GetChildren())
        {
            waves[boShu - 2].zrs[1].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType));
            node.QueueFree();
        }
        foreach (zombie_base node in zr3.GetChildren())
        {
            waves[boShu - 2].zrs[2].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType));
            node.QueueFree();
        }
    }
    void setZRnum(int index)
    {
        ZRnum = index;
        if (ZRnum == 0)
        {
            zr1.Visible = true;
            zr2.Visible = false;
            zr3.Visible = false;
        }
        else if (ZRnum == 1)
        {
            zr1.Visible = false;
            zr2.Visible = true;
            zr3.Visible = false;
        }
        else if (ZRnum == 2)
        {
            zr1.Visible = false;
            zr2.Visible = false;
            zr3.Visible = true;
        }
    }
}
