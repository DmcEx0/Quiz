using Quiz.Cell;
using Quiz.Factory;
using Quiz.Tool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quiz.Level
{
    public class GridBuilder : MonoBehaviour, IInitializeble
    {
        [SerializeField] private CellFactory _cellFactory;

        [Space]
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;

        private CellsInitializer _cellsInitializer;
        private LevelInitializer _levelInitializer;

        private List<CellElement> _cells;

        private int _previousWidth;
        private int _previousHeight;

        public event Action LevelComleted;

        [Inject]
        private void Construct(CellsInitializer cellsInitializer, LevelInitializer levelInitializer)
        {
            _cellsInitializer = cellsInitializer;
            _levelInitializer = levelInitializer;

            _levelInitializer.Add(this);

            _cells = new List<CellElement>();
        }

        public void BuildGrid(LevelConfig levelConfig)
        {
            for (int y = 0; y < levelConfig.Height; y++)
            {
                if (y >= _previousHeight)
                {
                    for (int x = 0; x < levelConfig.Width; x++)
                        PlaceableCell(x, y);
                }
                else
                {
                    for (int x = _previousWidth; x < levelConfig.Width; x++)
                        PlaceableCell(x, y);
                }
            }

            _cellsInitializer.SetCells(_cells);
            _cellsInitializer.RenderCells(levelConfig);

            SetPreviousValue(levelConfig.Width, levelConfig.Height);
        }

        public void Shutdown()
        {
            foreach (var cell in _cells)
            {
                Destroy(cell.gameObject);
            }

            _cells.Clear();

            _cellsInitializer.LevelComleted -= OnSelectedCorrectAnswer;

            _previousHeight = 0;
            _previousWidth = 0;
        }

        public void Initialize()
        {
            _cellsInitializer.LevelComleted += OnSelectedCorrectAnswer;
        }

        private void PlaceableCell(float x, float y)
        {
            CellElement cell = _cellFactory.Get();
            _cells.Add(cell);

            cell.transform.position = new Vector3(x * _offsetX, -y * _offsetY);
            cell.name = $"{x}/{y}";
        }

        private void SetPreviousValue(int width, int height)
        {
            _previousHeight = height;
            _previousWidth = width;
        }

        private void OnSelectedCorrectAnswer()
        {
            LevelComleted?.Invoke();
        }
    }
}