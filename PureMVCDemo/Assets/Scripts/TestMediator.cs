using PureMVC.Patterns;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMediator : Mediator
{

    public new const string NAME = "TestMediator";

    private Text levelText;
    private Button levelUpButton;

    public TestMediator(GameObject root) : base(NAME)
    {
        levelText = GameUtility.GetChildComponent<Text>(root, "Level");
        levelUpButton = GameUtility.GetChildComponent<Button>(root, "Button");

        levelUpButton.onClick.AddListener(OnClickLevelUpButton);
    }

    private void OnClickLevelUpButton()
    {
        Debug.Log("1. TestMediator -> OnClickLevelUpButton");
        SendNotification(NotificationConstant.LevelUp);
    }

    public override IList<string> ListNotificationInterests()
    {
        UnityEngine.Debug.Log("ListNotificationInterests");
        IList<string> list = new List<string>();
        list.Add(NotificationConstant.LevelChange);
        return list;
    }

    public override void HandleNotification(PureMVC.Interfaces.INotification notification)
    {
        UnityEngine.Debug.Log("HandleNotification");
        switch (notification.Name)
        {
            case NotificationConstant.LevelChange:
                CharacterInfo ci = notification.Body as CharacterInfo;
                levelText.text = ci.Level.ToString();
                break;
            default:
                break;
        }

    }
}