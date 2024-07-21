using Godot;
using System;
using System.IO;
using Newtonsoft.Json;

public partial class xuan_guan : Control
{
    [Export] public ItemList gqList;
    [Export] public OptionButton whichGtype;
    public PackedScene _nextScene;
    public guanQiaType gtp;
    public int currentSelectedType = -1;
    //public ConfigFile configFile = new ConfigFile();
    public void selectGuanQia(int index)//点击第几关
    {
        //GD.Print("index:" + index);
        saveContent readFromFile;
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
        string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string folderPath = Path.Combine(userDir, "PVZgd");
        StreamReader sr;
        string jsonString;
        _nextScene = GD.Load<PackedScene>("res://GameScene.tscn");
        Node nextSceneInstance = _nextScene.Instantiate();
        GetTree().CurrentScene.QueueFree();
        GetTree().Root.AddChild(nextSceneInstance);
        GetTree().CurrentScene = nextSceneInstance;
        nextSceneInstance.GetNode<Control>("Camera2D/CardUI").Visible = true;
        nextSceneInstance.GetNode<ScrollContainer>("Camera2D/zomCardUI/zomCardLeft").Visible = false;
        reciever dataRec = nextSceneInstance.GetNode<reciever>("reciever");
        switch (currentSelectedType)
        {
            case 0:
                gtp = guanQiaType.grassDay;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "grassDay"), "grassDay" + (index + 1) + ".json"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
                break;
            case 1:
                gtp = guanQiaType.grassNight;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "grassNight"), "grassNight" + (index + 1) + ".json"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
                break;
            case 2:
                gtp = guanQiaType.poolDay;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "poolDay"), "poolDay" + (index + 1) + ".json"));
                GD.Print(Path.Combine(Path.Combine(folderPath, "poolDay"), "poolDay" + (index + 1) + ".json"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
                break;
            case 3:
                gtp = guanQiaType.poolNight;
                sr = new StreamReader(Path.Combine(Path.Combine(Path.Combine(folderPath, "poolNight"), "poolNIght" + (index + 1) + ".json")));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
                break;
            case 4:
                gtp = guanQiaType.roof;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "roof"), "grassDay" + (index + 1) + ".json"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = JsonConvert.DeserializeObject<saveContent>(jsonString, setting);
                break;
            default:
                readFromFile = null;
                break;
        }
        dataRec.recieveData(gtp, 50, currentSelectedType, "name");
        dataRec.recvData(readFromFile);//传入关卡信息至接收器
    }
    public void selectType(int index)//选择关卡地图类型
    {
        GD.Print(index);
        currentSelectedType = index;
        string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string folderPath = Path.Combine(userDir, "PVZgd");
        DirectoryInfo d;
        FileSystemInfo[] fsinfos;
        switch (index)
        {
            case 0:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "grassDay"));
                fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                    }
                }
                break;
            case 1:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "grassNight"));
                fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                    }
                }
                break;
            case 2:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "poolDay"));
                //OS.ShellOpen(Path.Combine(folderPath, "poolDay"));
                fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                    }
                }
                break;
            case 3:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "poolNight"));
                fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                    }
                }
                break;
            case 4:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "roof"));
                fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                    }
                }
                break;
        }
    }
}
