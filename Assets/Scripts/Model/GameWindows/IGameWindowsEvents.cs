
using System;

namespace Quiz.Model.GameWindow
{
    public interface IGameWindowsEvents
    {
        public event Action GameOver;
        public event Action BootscreenShowed;
        public event Action WindowFilled;
    }
}