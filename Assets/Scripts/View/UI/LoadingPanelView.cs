using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelView : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private Color _loadingColor;

    public Image LoadingImage => _loadingImage;
    public TMP_Text LoadingText => _loadingText;

    private void Awake()
    {
        SetStartColor();
        SetEnablePanel(true);
    }

    public void SetStartColor()
    {
        _loadingImage.color = _loadingColor;
        _loadingText.alpha = 1.0f;
    }

    public void SetEnablePanel(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }
}