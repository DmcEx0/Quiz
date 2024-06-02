using Quiz.Config.Card;
using System;
using UnityEngine;

namespace Quiz.Config.Level
{
    [Serializable]
    public class LevelData
    {
        [SerializeField]
        private CardPackData _packCardData;

        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        public CardPackData PackCardData => _packCardData;
        public int Width => _width;
        public int Height => _height;
    }
}