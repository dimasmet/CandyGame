using UnityEngine;
using UnityEngine.UI;

public class GameView : ScreenPanel
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PauseView pauseView;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            pauseView.ShowPause();
        });
    }
}
