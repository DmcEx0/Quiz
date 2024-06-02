using System;

namespace Quiz.Model.GameWindow
{
    public class GameWindowsModel : IGameWindowsModel, IGameWindowsEvents
    {
        public event Action GameOver;
        public event Action BootscreenShowed;
        public event Action WindowFilled;

        public void FillWindowShowed()
        {
            WindowFilled?.Invoke();
        }

        public void OpenGameOverPanel()
        {
            GameOver?.Invoke();
        }

        public void ShowBootscreen()
        {
            BootscreenShowed?.Invoke();
        }
    }
}