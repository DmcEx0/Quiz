using Cysharp.Threading.Tasks;
using Quiz.Config.Level;
using Quiz.Model.GameWindow;
using Quiz.Model.Level;
using Quiz.Tool;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Quiz.Controllers
{
    public class LevelController : IAsyncStartable, IDisposable
    {
        private readonly CellViewPool _cellViewPool;
        private readonly LevelConfig _levelConfig;

        private readonly ILevelModel _levelModel;

        private readonly IGameWindowsEvents _gameWindowsEvenets;
        private readonly IGameWindowsModel _gameWindowsModel;

        private CancellationTokenSource _tokenSource;

        private int _currentLevelIndex = 0;

        public LevelController(LevelConfig levelConfig, ILevelModel levelModel, IGameWindowsEvents gameWindowsEvents,
            IGameWindowsModel gameWindowsModel, CellViewPool cellViewPool, AsyncAnimationProvider animationProvider)
        {
            _levelModel = levelModel;
            _levelConfig = levelConfig;
            _gameWindowsEvenets = gameWindowsEvents;
            _gameWindowsModel = gameWindowsModel;
            _cellViewPool = cellViewPool;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _tokenSource = new CancellationTokenSource();

            await RestartGameAsync();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private void ChooseNextLevelData()
        {
            if (_currentLevelIndex++ == _levelConfig.LevelsData.Count - 1)
            {
                _currentLevelIndex = 0;
                _gameWindowsModel.OpenGameOverPanel();

                return;
            }

            StartNextLevel();
        }

        private void StartNextLevel()
        {
            StartNextLevelAsync().Forget();

            async UniTask StartNextLevelAsync()
            {
                UnloadPool();

                await CreatePoolAsync(_tokenSource.Token);

                _levelModel.BuildGrid(_levelConfig.LevelsData[_currentLevelIndex], IsFirstLevel(_currentLevelIndex));
            }
        }

        private void RestartGame()
        {
            _tokenSource = new CancellationTokenSource();

            _levelModel.SelectedCorrectAnswer -= ChooseNextLevelData;

            _gameWindowsEvenets.WindowFilled -= UnloadPool;
            _gameWindowsEvenets.BootscreenShowed -= RestartGame;

            RestartGameAsync().Forget();
        }

        private async UniTask RestartGameAsync()
        {
            await CreatePoolAsync(_tokenSource.Token);

            _levelModel.SelectedCorrectAnswer += ChooseNextLevelData;

            _gameWindowsEvenets.WindowFilled += UnloadPool;
            _gameWindowsEvenets.BootscreenShowed += RestartGame;

            _levelModel.BuildGrid(_levelConfig.LevelsData[_currentLevelIndex], IsFirstLevel(_currentLevelIndex));
        }

        private bool IsFirstLevel(int levelIndex)
        {
            return levelIndex == 0;
        }

        private void UnloadPool()
        {
            _cellViewPool.Unload();
        }

        private async UniTask CreatePoolAsync(CancellationToken token)
        {
            int size = _levelConfig.LevelsData[_currentLevelIndex].Height * _levelConfig.LevelsData[_currentLevelIndex].Width;

            await _cellViewPool.Load(size, token);
        }
    }
}