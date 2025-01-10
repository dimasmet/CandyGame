using UnityEngine;
using UnityEngine.UI;

public class Settings : ScreenPanel
{
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _soundBtn;

    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    [SerializeField] private Button _tutorialBtn;

    [SerializeField] private Button _privacyBtn;
    [SerializeField] private Button _termsBtn;

    [Header("Reader")]
    [SerializeField] private GameObject _panelReader;
    [SerializeField] private Transform _container;
    [SerializeField] private Text _titleReaderText;
    [SerializeField] private Text _privacyText;
    [SerializeField] private Text _termsText;
    [SerializeField] private Button _closeReaderBtn;

    private void Awake()
    {
        _privacyBtn.onClick.AddListener(() =>
        {
            _panelReader.SetActive(true);
            _titleReaderText.text = "PRIVACY POLICY";
            _container.position = new Vector2(_container.position.x, 0);
            _privacyText.gameObject.SetActive(true);
            _termsText.gameObject.SetActive(false);
        });

        _termsBtn.onClick.AddListener(() =>
        {
            _panelReader.SetActive(true);
            _titleReaderText.text = "TERMS OF USE";
            _container.position = new Vector2(_container.position.x, 0);
            _privacyText.gameObject.SetActive(false);
            _termsText.gameObject.SetActive(true);
        });

        _closeReaderBtn.onClick.AddListener(() =>
        {
            _panelReader.SetActive(false);
        });

        Application.targetFrameRate = 90;

        _tutorialBtn.onClick.AddListener(() =>
        {
            GameRulesControl.I.ShowRules(true);
        });

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
