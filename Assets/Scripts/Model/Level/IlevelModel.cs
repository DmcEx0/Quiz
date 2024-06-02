using Quiz.Config.Card;
using Quiz.Config.Level;
using Quiz.Tool;
using System;

namespace Quiz.Model.Level
{
    public interface ILevelModel
    {
        public void BuildGrid(LevelData levelData, bool isFirstLevel);

        public void SelectCorrectAnswer();

        public event Action<CardPackData, int> StartedNextIteration;
        public event Action<int, int, bool> CellPlacing;

        public event Action SelectedCorrectAnswer;
    }
}