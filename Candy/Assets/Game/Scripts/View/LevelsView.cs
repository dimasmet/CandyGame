using UnityEngine;
using UnityEngine.UI;

public class LevelsView : ScreenPanel
{
    [SerializeField] private LevelButton[] lvlsBtn;

    [SerializeField] private Button _rulesBtn;

    [SerializeField] private Button _backBtn;

    private WrapLevels wrapLevels;

    public void InitButtons(WrapLevels wrapLevels)
    {
        this.wrapLevels = wrapLevels;
        UpdateViewButtons();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        UpdateViewButtons();
    }

    private void UpdateViewButtons()
    {
        for (int i = 0; i < lvlsBtn.Length; i++)
        {
            lvlsBtn[i].ContructInit(wrapLevels.levelInfos[i]);
        }
    }

    private void Awake()
    {
        _backBtn.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Menu);
        });

        _rulesBtn.onClick.AddListener(() =>
        {
            GameRulesControl.I.ShowRules(true);
        });
    }
}
