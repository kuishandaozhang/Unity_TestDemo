using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class TestProxy : Proxy
{

    public new const string NAME = "TestProxy";
    public CharacterInfo Data { get; set; }

    public TestProxy() : base(NAME)
    {
        Data = new CharacterInfo();
    }

    public void ChangeLevel(int change)
    {
        UnityEngine.Debug.Log("------------------------------TestProxy -> ChangeLevel");
        Data.Level += change;
        SendNotification(NotificationConstant.LevelChange, Data);
    }

}