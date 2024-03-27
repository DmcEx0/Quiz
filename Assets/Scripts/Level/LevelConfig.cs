using Quiz.CardData;
using System;
using UnityEngine;

namespace Quiz.Level
{
    [Serializable]
    public class LevelConfig
    {
        [SerializeField] private CardPackData _packCardData;

        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public CardPackData PackCardData => _packCardData;
        public int Width => _width;
        public int Height => _height;
    }
}