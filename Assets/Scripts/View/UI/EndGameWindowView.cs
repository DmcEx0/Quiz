using UnityEngine;
using UnityEngine.UI;

namespace Quiz.View.UI
{
    public class EndGameWindowView : MonoBehaviour
    {
        [SerializeField]
        private Image _backgroundImage;
        [SerializeField]
        private Image _bootScreenImage;
        [SerializeField]
        private Button _restartButton;

        public Image BackgroundImage => _backgroundImage;
        public Image BootScreenImage => _bootScreenImage;
        public Button RestartButton => _restartButton;
    }
}