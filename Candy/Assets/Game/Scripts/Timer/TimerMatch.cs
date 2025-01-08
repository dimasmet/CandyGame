using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerMatch : MonoBehaviour
{
    private float time;
    [SerializeField] private Text timerText;

    private float _timeLeft = 0f;

    private void Start()
    {
        Bootstrap.OnStartGame += StartTimer;
        Bootstrap.OnStopGame += StopTimer;
    }

    private void OnDisable()
    {
        Bootstrap.OnStartGame -= StartTimer;
        Bootstrap.OnStopGame -= StopTimer;
    }

    public void SetTime(float timeTarget)
    {
        time = timeTarget;
    }

    private IEnumerator WaitStartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }

        Bootstrap.OnEndTimeGame?.Invoke();
    }

    private void StartTimer()
    {
        StopAllCoroutines();
        _timeLeft = time;
        StartCoroutine(WaitStartTimer());
    }

    private void StopTimer()
    {
        StopAllCoroutines();
    }

    public float GetResultTime()
    {
        StopTimer();
        return _timeLeft;
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
