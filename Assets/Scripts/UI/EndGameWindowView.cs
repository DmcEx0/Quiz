using UnityEngine;
using UnityEngine.UI;

namespace Quiz.UI
{
    public class EndGameWindowView : UIView
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private Image _background;
        [SerializeField] private BootScreenView _bootScreenView;

        private void OnEnable()
        {
            AnimationProvider.CallFaidInEffect(_background, Duration, FromValue, TargetValue);
            _resetButton.onClick.AddListener(OnPressResetButton);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(OnPressResetButton);
        }

        private void OnPressResetButton()
        {
            _bootScreenView.ResetLevel();
        }
    }
}