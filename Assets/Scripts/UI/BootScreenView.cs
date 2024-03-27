using UnityEngine;
using UnityEngine.UI;

namespace Quiz.UI
{
    public class BootScreenView : UIView
    {
        [SerializeField] private Image _bootScreen;
        [SerializeField] private EndGameWindowView _endGameWindowView;

        public void ResetLevel()
        {
            _bootScreen.gameObject.SetActive(true);

            AnimationProvider.CallFaidInEffect(_bootScreen, Duration, FromValue, TargetValue, OnBootScreenAppears);
        }

        private void OnBootScreenAppears(bool isBootScreenAppear)
        {
            LevelInitializer.ShutdownAll();
            AnimationProvider.CallFaidInEffect(_bootScreen, Duration, TargetValue, FromValue, OnBootScreenFade);
            _endGameWindowView.gameObject.SetActive(false);
        }

        private void OnBootScreenFade(bool isBootScreenFade)
        {
            _bootScreen.gameObject.SetActive(false);
            LevelInitializer.InitializeAll();
        }
    }
}