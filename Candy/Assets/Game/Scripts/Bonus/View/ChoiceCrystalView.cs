using UnityEngine;
using UnityEngine.UI;

public class ChoiceCrystalView : MonoBehaviour
{
    public static ChoiceCrystalView I;

    [SerializeField] private Button _closeBtn;

    [SerializeField] private GameObject _panel;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        _closeBtn.onClick.AddListener(() =>
        {
            OpenPanel(false);
            ButtonBonus.globalActive = false;
        });

        Bootstrap.OnStartGame += ClosePanel;
    }

    private void OnDisable()
    {
        Bootstrap.OnStartGame -= ClosePanel;
    }

    private void ClosePanel()
    {
        OpenPanel(false);
    }

    public void OpenPanel(bool isOpen)
    {
        _panel.SetActive(isOpen);
    }

    public void ChoicedCrystal(Sprite spriteCrystal)
    {
        OpenPanel(false);
        BonusesHandler.I.ActiveBonus(2, BonusesHandler.NameBonus.BonusRemoveСrystals, spriteCrystal);
    }
}
