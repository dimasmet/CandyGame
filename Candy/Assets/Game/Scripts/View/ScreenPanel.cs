using UnityEngine;

public class ScreenPanel : MonoBehaviour
{
    [SerializeField] private GameObject _screen;

    public virtual void OpenPanel()
    {
        _screen.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        _screen.SetActive(false);
    }
}
