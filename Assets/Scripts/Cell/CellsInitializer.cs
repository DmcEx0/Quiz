using Quiz.Level;
using Quiz.Tool;
using Quiz.UI;
using System;
using System.Collections.Generic;

namespace Quiz.Cell
{
    public class CellsInitializer : IInitializeble
    {
        private readonly FindingTextElementUI _findingText;
        private readonly LevelInitializer _levelInitializer;

        private IReadOnlyCollection<CellElement> _cells;

        private bool _isInitialized;

        public event Action LevelComleted;

        public CellsInitializer(FindingTextElementUI findingTextElement, LevelInitializer levelInitializer)
        {
            _levelInitializer = levelInitializer;
            _findingText = findingTextElement;
            _levelInitializer.Add(this);
        }

        public void Initialize()
        {
            _isInitialized = true;
        }

        public void Shutdown()
        {
            _isInitialized = false;
        }

        public void SetCells(IReadOnlyCollection<CellElement> cells)
        {
            _cells = cells;
        }

        public void RenderCells(LevelConfig levelConfig)
        {
            int randomIndexCorrectAnswer = UnityEngine.Random.Range(0, _cells.Count);
            int index = 0;

            foreach (CellElement cell in _cells)
            {
                if (index++ == randomIndexCorrectAnswer)
                {
                    cell.SetTrueCorrectAnswer();
                    cell.SelectedCorrectAnswer += OnSelectedCorrectAnswer;
                }

                cell.Enable(_isInitialized);
                cell.Renderer(levelConfig.PackCardData);

                if (cell.IsCorrectAnswer)
                    SelectSignText(cell);
            }
        }

        private void ResetCells()
        {
            foreach (var cell in _cells)
            {
                cell.SelectedCorrectAnswer -= OnSelectedCorrectAnswer;
                cell.ResetCellFiller();
                cell.Disable();
            }
        }

        private void SelectSignText(CellElement cell)
        {
            _findingText.ChangeFindingSign(cell.Identifier);
        }

        private void OnSelectedCorrectAnswer()
        {
            ResetCells();

            LevelComleted?.Invoke();
        }
    }
}