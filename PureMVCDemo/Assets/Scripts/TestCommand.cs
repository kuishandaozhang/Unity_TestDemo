using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class TestCommand : SimpleCommand
{

    public new const string NAME = "TestCommand";

    public override void Execute(PureMVC.Interfaces.INotification notification)
    {
        TestProxy proxy = (TestProxy)Facade.RetrieveProxy(TestProxy.NAME);
        Debug.Log("TestCommand");
        proxy.ChangeLevel(1);
    }
}