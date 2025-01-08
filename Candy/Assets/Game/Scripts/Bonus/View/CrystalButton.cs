using UnityEngine;
using UnityEngine.UI;

public class CrystalButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _spriteCrystal;

    [SerializeField] private Image _image;

    private void Start()
    {
        _image.sprite = _spriteCrystal;
    }

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            ChoiceCrystalView.I.ChoicedCrystal(_spriteCrystal);
        });
    }
}
