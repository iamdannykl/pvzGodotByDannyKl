using Godot;
using System.Text.RegularExpressions;
using System;
public enum guanQiaType
{
    grassDay,
    grassNight,
    poolDay,
    poolNight,
    roof
}
public partial class Finish : Button
{
    private PackedScene _nextScene;
    [Export] public OptionButton mapType;
    [Export] public LineEdit sunOriginal;
    [Export] public OptionButton modeType;
    public guanQiaType guanQiaType;
    public int mapTypeIndex = -1;
    public int modeTypeIndex = -1;
    public override void _Ready()
    {
        // 加载目标场景
        _nextScene = (PackedScene)ResourceLoader.Load("res://grassDay.tscn");
        Connect("button_up", new Callable(this, nameof(SwitchScene)));
        mapType.Connect("item_selected", new Callable(this, nameof(getMapIndex)));
        modeType.Connect("item_selected", new Callable(this, nameof(getModeIndex)));
    }
    void getMapIndex(int index)
    {
        mapTypeIndex = index;
    }
    void getModeIndex(int index)
    {
        modeTypeIndex = index;
    }

    public void SwitchScene(/* string parameter */)
    {
        if (!(mapTypeIndex > -1 && Regex.IsMatch(sunOriginal.Text, @"\d") && !(Regex.Matches(sunOriginal.Text, "[a-zA-Z]").Count > 0))) return;
        // 实例化目标场景
        Node nextSceneInstance = _nextScene.Instantiate();

        // 传递参数给目标场景的根节点或其他特定节点
        /* if (nextSceneInstance is NextSceneRootNode rootNode)
        {
            rootNode.ReceiveParameter(parameter);
        } */

        // 将新场景添加到当前场景树并移除当前场景
        GetTree().CurrentScene.QueueFree();
        addDatasToMap(sunOriginal.Text);//给新场景传输数据
        nextSceneInstance.GetNode<reciever>("reciever").recieveData(guanQiaType, sunOriginal.Text.ToInt(), mapTypeIndex);
        GetTree().Root.AddChild(nextSceneInstance);
        //nextSceneInstance.GetNode<Label>("Label").Text = "asd";
        GetTree().CurrentScene = nextSceneInstance;
    }
    public void addDatasToMap(string sunStr)
    {
        if (mapTypeIndex > -1 && Regex.IsMatch(sunStr, @"\d") && !(Regex.Matches(sunStr, "[a-zA-Z]").Count > 0))
        {
            switch (mapTypeIndex)
            {
                case 0:
                    guanQiaType = guanQiaType.grassDay;
                    break;
                case 1:
                    guanQiaType = guanQiaType.grassNight;
                    break;
                case 2:
                    guanQiaType = guanQiaType.poolDay;
                    break;
                case 3:
                    guanQiaType = guanQiaType.poolNight;
                    break;
                case 4:
                    guanQiaType = guanQiaType.roof;
                    break;
            }
        }
        else
        {

        }
    }
}
