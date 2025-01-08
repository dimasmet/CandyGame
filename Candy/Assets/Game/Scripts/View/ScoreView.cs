using UnityEngine;
using UnityEngine.UI;

public class ScoreView
{
    private Text _scoreText;
    private Text _scoreTextTarget;
    private Animator _animText;

    public ScoreView(Text textField, Text scoreTargetText)
    {
        _scoreText = textField;
        _scoreTextTarget = scoreTargetText;
        _animText = _scoreText.transform.GetComponent<Animator>();
    }

    public void UpdateTextView(int valueScore, int targetScore)
    {
        _scoreText.text = valueScore.ToString();
        _scoreTextTarget.text = "/" + targetScore.ToString();
        _animText.Play("Scale");
    }
}
