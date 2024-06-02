using Quiz.Config.Card;
using Quiz.Config.Level;
using Quiz.Tool;
using System;

namespace Quiz.Model.Level
{
    public class LevelModel : ILevelModel
    {
        private readonly CameraCentrer _cameraCentrer;

        public event Action<CardPackData, int> StartedNextIteration;
        public event Action<int, int, bool> CellPlacing;

        public event Action SelectedCorrectAnswer;

        public LevelModel(CameraCentrer cameraCentrer)
        {
            _cameraCentrer = cameraCentrer;
        }

        public void BuildGrid(LevelData levelData, bool IsFirstLevel)
        {
            int size = levelData.Width * levelData.Height;
            StartedNextIteration?.Invoke(levelData.PackCardData, size);

            for (int y = 0; y < levelData.Height; y++)
            {
                for (int x = 0; x < levelData.Width; x++)
                {
                    CellPlacing?.Invoke(x, y, IsFirstLevel);
                }
            }

            _cameraCentrer.CenteringCamera(levelData.Width, levelData.Height);
        }

        public void SelectCorrectAnswer()
        {
            SelectedCorrectAnswer?.Invoke();
        }
    }
}