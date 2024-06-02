using Cysharp.Threading.Tasks;
using Quiz.Config.Animation;
using Quiz.Model.GameWindow;
using Quiz.Tool;
using Quiz.View.UI;
using System;
using System.Threading;
using VContainer.Unity;

namespace Quiz.Controllers
{
    public class GameWindowController : IInitializable, IDisposable
    {
        private readonly AsyncAnimationProvider _animationProvider;

        private readonly EndGameWindowView _endGameWindowView;
        private readonly FindingTextView _findingTextView;
        private readonly IGameWindowsModel _gameWindowsModel;
        private readonly IGameWindowsEvents _gameWindowsEvents;

        private CancellationTokenSource _tokenSource;

        public GameWindowController(EndGameWindowView endGameWindowView, IGameWindowsModel gameWindowsModel,
            IGameWindowsEvents gameWindowsEvents, AsyncAnimationProvider animationProvider, FindingTextView findingTextView)
        {
            _endGameWindowView = endGameWindowView;
            _gameWindowsModel = gameWindowsModel;
            _gameWindowsEvents = gameWindowsEvents;
            _animationProvider = animationProvider;
            _findingTextView = findingTextView;
        }

        public void Initialize()
        {
            _tokenSource = new CancellationTokenSource();

            _findingTextView.Text.alpha = 0f;

            _gameWindowsEvents.GameOver += ShowEndGameWindowPanel;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();

            _gameWindowsEvents.GameOver -= ShowEndGameWindowPanel;
        }

        private void ShowEndGameWindowPanel()
        {
            ShowEndGameWindowPanelAsync().Forget();

            async UniTask ShowEndGameWindowPanelAsync()
            {
                _endGameWindowView.gameObject.SetActive(true);

                await _animationProvider.CallFadeInEffectImageAsync(_endGameWindowView.BackgroundImage, AnimationsType.AppearsEndGameWidow, _tokenSource.Token);

                _endGameWindowView.RestartButton.onClick.AddListener(ShowBootScreenWindow);
            }
        }

        private void ShowBootScreenWindow()
        {
            _endGameWindowView.RestartButton.onClick.RemoveListener(ShowBootScreenWindow);

            ShowBootScreenWindowAsync().Forget();

            async UniTask ShowBootScreenWindowAsync()
            {
                await _animationProvider.CallFadeInEffectImageAsync(_endGameWindowView.BootScreenImage, AnimationsType.AppearsBootscreen, _tokenSource.Token);

                _endGameWindowView.gameObject.SetActive(false);

                _gameWindowsModel.FillWindowShowed();
                _findingTextView.Text.alpha = 0;

                await _animationProvider.CallFadeInEffectImageAsync(_endGameWindowView.BootScreenImage, AnimationsType.FaidInBootscreen, _tokenSource.Token);

                _gameWindowsModel.ShowBootscreen();
            }
        }
    }
}