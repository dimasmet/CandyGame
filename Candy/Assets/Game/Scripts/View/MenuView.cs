using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : ScreenPanel
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _settingsBtn;

    private void Awake()
    {
        _playBtn.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Levels);

            if (PlayerPrefs.GetInt("ShowFirstRules") != 1)
            {
                GameRulesControl.I.ShowRules(false);
                PlayerPrefs.SetInt("ShowFirstRules", 1);
            }
        });

        _settingsBtn.onClick.AddListener(() =>
        {
            //ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.);
        });
    }
}
