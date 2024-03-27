using Quiz.Cell;
using Quiz.Level;
using Quiz.UI;
using UnityEngine;
using Zenject;

namespace Quiz.Tool
{
    public class GameInstallers : MonoInstaller
    {
        [SerializeField] private UIView _findingText;

        public override void InstallBindings()
        {
            Container.Bind<LevelInitializer>().AsSingle();

            Container.Bind<CellFiller>().AsSingle();

            Container.Bind<AnimationProvider>().AsSingle();

            Container.Bind<CellsInitializer>().AsSingle().WithArguments(_findingText);
            Container.BindInstance(_findingText).AsSingle();
        }
    }
}