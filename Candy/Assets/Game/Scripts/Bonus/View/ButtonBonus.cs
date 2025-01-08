using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBonus : MonoBehaviour
{
    private int number;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _thisButton;
    [SerializeField] private Outline _outline;

    private BonusData _bonusData;
    private BonusesHandler _handler;

    public static bool globalActive = false;

    public enum StateButton
    {
        None,
        Active,
        Null
    }

    private void Awake()
    {
        _thisButton.onClick.AddListener(() =>
        {
            Debug.Log(globalActive);
            if (globalActive == false)
            {
                globalActive = true;
                if (_bonusData.nameBonus == BonusesHandler.NameBonus.BonusRemoveСrystals)
                {
                    ChoiceCrystalView.I.OpenPanel(true);
                }
                else
                {
                    _handler.ActiveBonus(number, _bonusData.nameBonus);
                    Debug.Log("_bonusData.nameBonus " + _bonusData.nameBonus);
                }
            }
        });
    }

    public void InitButton(BonusData bonusData, BonusesHandler bonusesHandler)
    {
        _bonusData = bonusData;
        _handler = bonusesHandler;
        number = _bonusData.numberBonus;
        UpdateInfo();
        //UpdateState(StateButton.None);
    }

    private void OnEnable()
    {
        if (_bonusData != null)
        {
            UpdateInfo();
        }
    }

    public void UpdateInfo()
    {
        _countText.text = _bonusData.countBonus.ToString();
        UpdateState(StateButton.None);
    }

    public void UpdateState(StateButton state)
    {
        switch (state)
        {
            case StateButton.None:
                if (_bonusData.countBonus <= 0)
                {
                    _thisButton.interactable = false;
                    _outline.enabled = false;
                }
                else
                {
                    _thisButton.interactable = true;
                    _outline.enabled = false;
                }
                break;
            case StateButton.Active:
                _thisButton.interactable = false;
                _outline.enabled = true;
                break;
            case StateButton.Null:
                _thisButton.interactable = false;
                _outline.enabled = false;
                break;
        }
    }
}
