using Quiz.Tool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quiz.Level
{
    public class LevelProvider : MonoBehaviour, IInitializeble
    {
        [SerializeField] private CameraCentrer _cameraCentrer;
        [SerializeField] private GridBuilder _gridBuilder;

        [Space]
        [SerializeField] private List<LevelConfig> _levelConfigs;

        private LevelInitializer _levelInitializer;

        private LevelConfig _currentLevelConfig;
        private int _currentLevelIndex = 0;

        public event Action LevelsOver;

        [Inject]
        private void Construct(LevelInitializer levelInitializer)
        {
            _levelInitializer = levelInitializer;
            _levelInitializer.Add(this);
        }

        private void Awake()
        {
            _levelInitializer.InitializeAll();
        }

        public void Initialize()
        {
            SelectNextLevel();
            _gridBuilder.LevelComleted += SelectNextLevel;
        }

        public void Shutdown()
        {
            _currentLevelConfig = null;
            _currentLevelIndex = 0;
            _gridBuilder.LevelComleted -= SelectNextLevel;
        }

        private void SelectNextLevel()
        {
            if (_currentLevelIndex == _levelConfigs.Count)
            {
                FinishedGame();
                return;
            }

            _currentLevelConfig = _levelConfigs[_currentLevelIndex++];
            _gridBuilder.BuildGrid(_currentLevelConfig);
            _cameraCentrer.CenteringCamera(_currentLevelConfig.Width, _currentLevelConfig.Height);
        }

        private void FinishedGame()
        {
            LevelsOver?.Invoke();
        }
    }
}