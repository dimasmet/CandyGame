using UnityEngine;
using UnityEngine.UI;

public class StoreHandler : ScreenPanel
{
    public static StoreHandler I;

    private WrapperDataBonus wrapperDataBonus;

    [SerializeField] private Text _countBonus1;
    [SerializeField] private Text _countBonus2;
    [SerializeField] private Text _countBonus3;

    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    private void Awake()
    {
        if ( I == null)
        {
            I = this;
        }

        _openBtn.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Store);
            UpdateData();
        });

        _closeBtn.onClick.AddListener(() =>
        {
            ScreensViewHandler.UI.OpenPanel(ScreensViewHandler.NamePanel.Menu);
        });
    }

    public void InitStore(WrapperDataBonus wrapperData)
    {
        wrapperDataBonus = wrapperData;

        UpdateData();
    }

    public void BuySuccessItem(int numBonus)
    {
        wrapperDataBonus.bonusDatas[numBonus].countBonus++;
        UpdateData();
    }

    private void UpdateData()
    {
        _countBonus1.text = wrapperDataBonus.bonusDatas[0].countBonus.ToString();
        _countBonus2.text = wrapperDataBonus.bonusDatas[1].countBonus.ToString();
        _countBonus3.text = wrapperDataBonus.bonusDatas[2].countBonus.ToString();
    }
}
