using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulesControl : MonoBehaviour
{
    public static GameRulesControl I;

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject[] _rulesPanels;
    private int number;

    [SerializeField] private Text _numberStepText;
    [SerializeField] private Button _okBtn;

    private GameObject _currentActive;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        _okBtn.onClick.AddListener(() => {
            NextStepRules();
        });
    }

    public void ShowRules(bool isUserInvoke)
    {
        number = 0;

        if (_currentActive != null) _currentActive.SetActive(false);

        _currentActive = _rulesPanels[number];
        _currentActive.SetActive(true);

        _mainPanel.SetActive(true);

        _numberStepText.text = (number +1) + "/5";
    }

    private void NextStepRules()
    {
        number++;

        if (number < _rulesPanels.Length)
        {
            _numberStepText.text = (number + 1) + "/5";
            if (_currentActive != null) _currentActive.SetActive(false);

            _currentActive = _rulesPanels[number];
            _currentActive.SetActive(true);
        }
        else
        {
            _mainPanel.SetActive(false);
        }
    }
}
