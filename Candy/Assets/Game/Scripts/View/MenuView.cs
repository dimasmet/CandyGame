using UnityEngine;
using UnityEngine.UI;

public class MenuView : ScreenPanel
{
    [SerializeField] private Button _playBtn;

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
    }
}
