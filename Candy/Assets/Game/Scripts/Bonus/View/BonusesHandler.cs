using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WrapperDataBonus
{
    public BonusData[] bonusDatas;

    public void SaveData()
    {
        string strJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("BonusesData", strJson);
    }

    public WrapperDataBonus GetData()
    {
        string strJson = PlayerPrefs.GetString("BonusesData");

        WrapperDataBonus dataBonus = this;

        if (strJson != "")
            dataBonus = JsonUtility.FromJson<WrapperDataBonus>(strJson);

        return dataBonus;
    }
}

public class BonusesHandler : MonoBehaviour
{
    public static BonusesHandler I;

    [SerializeField] private ButtonBonus[] _buttonsBonus;
    [SerializeField] private WrapperDataBonus _wrapperDataBonus;

    private int curNumBonus;
    private bool activeBonus = false;

    public enum NameBonus
    {
        None,
        BonusNewField,
        BonusHammer,
        BonusRemoveСrystals
    }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
    }

    private void Start()
    {
        _wrapperDataBonus = _wrapperDataBonus.GetData();

        for (int i = 0; i < _buttonsBonus.Length; i++)
        {
            _buttonsBonus[i].InitButton(_wrapperDataBonus.bonusDatas[i], this);
        }

        StoreHandler.I.InitStore(_wrapperDataBonus);
    }

    public void SaveData()
    {
        _wrapperDataBonus.SaveData();
    }

    public void ActiveBonus(int number, NameBonus bonus, Sprite crystalSprite = null)
    {
        Debug.Log("1 ---- ButtonBonus.globalActive " + ButtonBonus.globalActive);
        if (activeBonus == false)
        {
            curNumBonus = number;

            switch (bonus)
            {
                case NameBonus.BonusNewField:
                    Debug.Log("New Field");
                    BoardManager.instance.RespawnField(false);
                    ButtonBonus.globalActive = true;
                    Invoke(nameof(ActionCompleted), 0.5f);
                    break;
                case NameBonus.BonusHammer:
                    HandlerMatch.Instance.ActiveBonusHammer();
                    break;
                case NameBonus.BonusRemoveСrystals:
                    BoardManager.instance.DestroyCrystalSprite(crystalSprite);
                    ButtonBonus.globalActive = true;
                    Invoke(nameof(ActionCompleted), 0.5f);
                    break;
            }

            _buttonsBonus[curNumBonus].UpdateState(ButtonBonus.StateButton.Active);

            activeBonus = true;
        }

        Debug.Log("2 ---- ButtonBonus.globalActive " + ButtonBonus.globalActive);
    }

    public void ActionCompleted()
    {
        activeBonus = false;
        _buttonsBonus[curNumBonus].UpdateState(ButtonBonus.StateButton.None);

        _wrapperDataBonus.bonusDatas[curNumBonus].countBonus--;
        _buttonsBonus[curNumBonus].UpdateInfo();

        SoundsHandler.sound.PlayShotSound(SoundsHandler.NameSoundGame.Boom);

        ButtonBonus.globalActive = false;

        SaveData();

        Debug.Log("3 ---- ButtonBonus.globalActive " + ButtonBonus.globalActive);
    }
}
