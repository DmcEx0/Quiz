using System.Runtime.InteropServices;
using Quiz.Config.Animation;
using Quiz.Config.Level;
using Quiz.Controllers;
using Quiz.Factory;
using Quiz.Model.Cell;
using Quiz.Model.GameWindow;
using Quiz.Model.Level;
using Quiz.View.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Quiz.Tool.Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private EndGameWindowView _endGameWindowView;
        [SerializeField] private FindingTextView _findingText;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private AssetReference _cellPrefab;
        [SerializeField] private ColorFillConfig _colorFillConfig;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private AnimationsConfig _animationsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigs(builder);
            RegisterGameWindow(builder);
            RegisterLevel(builder);
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterComponent(_levelConfig);
            builder.RegisterComponent(_cellPrefab);
            builder.RegisterComponent(_colorFillConfig);
            builder.RegisterComponent(_animationsConfig);
        }

        private void RegisterGameWindow(IContainerBuilder builder)
        {
            builder.RegisterComponent(_endGameWindowView);
            builder.RegisterComponent(_findingText);

            builder.Register<GameWindowsModel>(Lifetime.Singleton).As<IGameWindowsModel, IGameWindowsEvents>();
            builder.RegisterEntryPoint<GameWindowController>();
        }

        private void RegisterLevel(IContainerBuilder builder)
        {
            builder.Register<LevelModel>(Lifetime.Singleton).As<ILevelModel>();
            builder.Register<AssetsFactory>(Lifetime.Singleton);
            builder.Register<CellModel>(Lifetime.Singleton).As<ICellModel>();
            builder.Register<CellViewPool>(Lifetime.Singleton);
            builder.Register<CameraCentrer>(Lifetime.Singleton);
            builder.Register<AsyncAnimationProvider>(Lifetime.Singleton);

            builder.RegisterEntryPoint<LevelController>().WithParameter(_mainCamera);
            builder.RegisterEntryPoint<PlayerInputController>();
            builder.RegisterEntryPoint<CellsController>();
        }
    }
}