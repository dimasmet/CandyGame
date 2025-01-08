using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : ScreenPanel
{
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _soundBtn;

    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        _openBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(true);
        });

        _closeBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(false);
        });

        _musicBtn.onClick.AddListener(() =>
        {
            bool st = SoundsHandler.sound.ActiveMusic();

            if (st) _musicBtn.transform.GetChild(0).GetComponent<Text>().text = "ON";
            else _musicBtn.transform.GetChild(0).GetComponent<Text>().text = "OFF";
        });

        _soundBtn.onClick.AddListener(() =>
        {
            bool st = SoundsHandler.sound.ActiveSounds();

            if (st) _soundBtn.transform.GetChild(0).GetComponent<Text>().text = "ON";
            else _soundBtn.transform.GetChild(0).GetComponent<Text>().text = "OFF";
        });
    }
}
