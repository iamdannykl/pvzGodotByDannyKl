using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
public partial class GuanQiaSaver : Control
{
    public int ZRnum = -1;
    [Export] public reciever dataRec;
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
    void saveCurrentBo()
    {
        //waves.Add(new wave());
        if (zr1.GetChildren().Count == 0 && zr2.GetChildren().Count == 0 && zr3.GetChildren().Count == 0)
        {
            return;
        }
        if (ZRnum != -1)
        {
            waveNum.Text = "第" + (boShu) + "波";
        }
        foreach (zombie_base node in zr1.GetChildren())
        {
            waves[boShu - 1].zrs[0].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
            //GD.Print(waves[boShu -1].zrs[0].zomInfos.Count());
            node.QueueFree();
        }
        foreach (zombie_base node in zr2.GetChildren())
        {
            waves[boShu - 1].zrs[1].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
            node.QueueFree();
        }
        foreach (zombie_base node in zr3.GetChildren())
        {
            waves[boShu - 1].zrs[2].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
            node.QueueFree();
        }
    }
    void FinishSave()
    {
        saveCurrentBo();
        if (zr1.GetChildren().Count == 0 && zr2.GetChildren().Count == 0 && zr3.GetChildren().Count == 0)
        {
            return;
        }
        saveContent save = new saveContent(waves, dataRec.thisGuanQiaType);
        GD.Print(save.waves.Count);


        /* jsonString = JsonConvert.SerializeObject(save, settings); */


        // 使用 MessagePack 序列化
        //byte[] msgPackBytes = MessagePackSerializer.Serialize(save);
        /* YourObject deserializedObject = MessagePackSerializer.Deserialize<YourObject>(msgPackBytes);
 */
        var serializer = new SerializerBuilder().Build();
        string msgPackBytes = serializer.Serialize(save);
        GD.Print(1 + danli.Instance.getGq(dataRec.thisGuanQiaType));
        //danli.Instance.addGq(dataRec.thisGuanQiaType, 1 + danli.Instance.getGq(dataRec.thisGuanQiaType));
        string path;
        string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string folderPath = Path.Combine(userDir, "PVZgd");
        int gqs = 0;
        DirectoryInfo d;
        FileSystemInfo[] fsinfos;
        switch (dataRec.thisGuanQiaType)
        {
            case guanQiaType.grassDay:
                d = new DirectoryInfo(Path.Combine(folderPath, "grassDay"));
                fsinfos = d.GetFileSystemInfos();
                if (fsinfos != null && fsinfos.Length > 0)
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        if (!(fsinfo is DirectoryInfo) && (fsinfo != null))     //判断是否为文件夹
                        {
                            GD.Print("1 file found!" + fsinfo.FullName);
                            gqs++;
                        }
                    }
                path = Path.Combine(folderPath, "grassDay");
                path = Path.Combine(path, "grassDay" + (gqs + 1) + ".yaml");
                break;
            case guanQiaType.grassNight:
                d = new DirectoryInfo(Path.Combine(folderPath, "grassNight"));
                fsinfos = d.GetFileSystemInfos();
                if (fsinfos != null && fsinfos.Length > 0)
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        if (!(fsinfo is DirectoryInfo) && (fsinfo != null))     //判断是否为文件夹
                        {
                            GD.Print("1 file found!" + fsinfo.FullName);
                            gqs++;
                        }
                    }
                path = Path.Combine(folderPath, "grassNight");
                path = Path.Combine(path, "grassNight" + (gqs + 1) + ".yaml");
                break;
            case guanQiaType.poolDay:
                d = new DirectoryInfo(Path.Combine(folderPath, "poolDay"));
                fsinfos = d.GetFileSystemInfos();
                if (fsinfos != null && fsinfos.Length > 0)
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        if (!(fsinfo is DirectoryInfo) && (fsinfo != null))     //判断是否为文件夹
                        {
                            GD.Print("1 file found!" + fsinfo.FullName);
                            gqs++;
                        }
                    }
                path = Path.Combine(folderPath, "poolDay");
                path = Path.Combine(path, "poolDay" + (gqs + 1) + ".yaml");
                break;
            case guanQiaType.poolNight:
                d = new DirectoryInfo(Path.Combine(folderPath, "poolNight"));
                fsinfos = d.GetFileSystemInfos();
                if (fsinfos != null && fsinfos.Length > 0)
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        if (!(fsinfo is DirectoryInfo) && (fsinfo != null))     //判断是否为文件夹
                        {
                            GD.Print("1 file found!" + fsinfo.FullName);
                            gqs++;
                        }
                    }
                path = Path.Combine(folderPath, "poolNight");
                path = Path.Combine(path, "poolNight" + (gqs + 1) + ".yaml");
                break;
            case guanQiaType.roof:
                d = new DirectoryInfo(Path.Combine(folderPath, "roof"));
                fsinfos = d.GetFileSystemInfos();
                if (fsinfos != null && fsinfos.Length > 0)
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        if (!(fsinfo is DirectoryInfo) && (fsinfo != null))     //判断是否为文件夹
                        {
                            GD.Print("1 file found!" + fsinfo.FullName);
                            gqs++;
                        }
                    }
                path = Path.Combine(folderPath, "roof");
                path = Path.Combine(path, "roof" + (gqs + 1) + ".yaml");
                break;
            default:
                path = "sss";
                break;
        }
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        //FileStream fileStream = new FileStream(path, FileMode.Create);
        StreamWriter sw = new StreamWriter(path);
        //sw.Write(jsonString);
        sw.Write(msgPackBytes);
        sw.Close();
    }
    /* public void deSerializeJsonToObject()
    {
        StreamReader sr = new StreamReader("user://");
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
    } */
    void clickNextButton()
    {
        if (zr1.GetChildren().Count == 0 && zr2.GetChildren().Count == 0 && zr3.GetChildren().Count == 0)
        {
            return;
        }
        waves.Add(new wave());
        if (ZRnum != -1)
        {
            waveNum.Text = "第" + (++boShu) + "波";
        }
        foreach (zombie_base node in zr1.GetChildren())
        {
            waves[boShu - 2].zrs[0].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
            //GD.Print(waves[boShu - 2].zrs[0].zomInfos.Count());
            node.QueueFree();
        }
        foreach (zombie_base node in zr2.GetChildren())
        {
            waves[boShu - 2].zrs[1].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
            node.QueueFree();
        }
        foreach (zombie_base node in zr3.GetChildren())
        {
            waves[boShu - 2].zrs[2].zomInfos.Add(new zomInfo(node.weiZhi, node.zomType, node.hangNum));
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
