using Quiz.Config.Card;
using Quiz.View.Cell;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz.Model.Cell
{
    public class CellModel : ICellModel
    {
        private const float AngleToRotate = -90f;
        private const float Offset = 0.15f;

        private readonly CellViewPool _pool;
        private readonly ColorFillConfig _colorFillConfig;

        private Queue<Color> _colorForCurrentIteration;

        private List<CardElementData> _commonCurrentIterationListCard;
        private List<CardElementData> _usedCorrectAnswer;

        private List<CardElementData> _cardsToChooseForCurrentIteration;

        private CardElementData _currentCorrectAnswer;

        private int _currentIndex = 0;
        private int randomIndexForCorrectCard;

        public CellModel(CellViewPool pool, ColorFillConfig colorFillConfig)
        {
            _pool = pool;
            _colorFillConfig = colorFillConfig;
        }

        public void Initialize()
        {
            _usedCorrectAnswer = new List<CardElementData>();
            _cardsToChooseForCurrentIteration = new List<CardElementData>();
        }

        public void StartNextIteration(CardPackData cardPackData, int requiredAmount)
        {
            DefineCardList(cardPackData);
            DefineColorList();

            _currentIndex = 0;
            randomIndexForCorrectCard = Random.Range(1, requiredAmount + 1);
        }

        public CellView GetNewCellForPlace(int posX, int posY)
        {
            var currentData = GetCardDataForNewCell();

            var newCell = _pool.Get();

            newCell.gameObject.SetActive(true);

            newCell.CellSpriteRenderer.color = GetCellColor();
            newCell.CardTransform.rotation = Quaternion.identity;
            newCell.transform.position = new Vector3(posX * (newCell.transform.localScale.x - Offset), posY * (newCell.transform.localScale.y - Offset));
            newCell.CardSpriteRenderer.sprite = currentData.Sprite;

            newCell.SetIdentifier(currentData.Identifier);

            if (currentData.NeedRotate)
            {
                newCell.CardTransform.Rotate(0, 0, AngleToRotate);
            }

            return newCell;
        }

        private CardElementData GetCardDataForNewCell()
        {
            _currentIndex++;
            CardElementData newCard;

            if (_currentIndex == randomIndexForCorrectCard)
            {
                newCard = _currentCorrectAnswer;
                _commonCurrentIterationListCard.Remove(newCard);

                return newCard;
            }

            newCard = _commonCurrentIterationListCard[0];

            _commonCurrentIterationListCard.Remove(newCard);

            return newCard;
        }

        public string GetCurrentCorrectAnswer()
        {
            return _currentCorrectAnswer.Identifier;
        }

        private void DefineCardList(CardPackData cardPackData)
        {
            _cardsToChooseForCurrentIteration.Clear();

            _commonCurrentIterationListCard = new List<CardElementData>(cardPackData.Cards);

            var notUsedCards = _commonCurrentIterationListCard.Except(_usedCorrectAnswer);
            var cardElementDatas = notUsedCards as CardElementData[] ?? notUsedCards.ToArray();

            if (cardElementDatas.Count() == 1)
            {
                _usedCorrectAnswer.Clear();
            }

            _cardsToChooseForCurrentIteration.AddRange(cardElementDatas);

            _currentCorrectAnswer = _cardsToChooseForCurrentIteration[Random.Range(0, _cardsToChooseForCurrentIteration.Count())];
            _usedCorrectAnswer.Add(_currentCorrectAnswer);

            _commonCurrentIterationListCard.Remove(_currentCorrectAnswer);

            Shuffle(_commonCurrentIterationListCard);

            _commonCurrentIterationListCard.Add(_currentCorrectAnswer);
        }

        private void DefineColorList()
        {
            _colorForCurrentIteration = new Queue<Color>(_colorFillConfig.ColorFrames);
        }

        private Color GetCellColor()
        {
            if (_colorForCurrentIteration.Count == 0)
            {
                DefineColorList();
            }

            return _colorForCurrentIteration.Dequeue();
        }

        private void Shuffle<T>(IList<T> cards)
        {
            int index = cards.Count;

            while (index > 1)
            {
                index--;

                int randomElement = Random.Range(0, index + 1);

                var card = cards[randomElement];

                cards[randomElement] = cards[index];
                cards[index] = card;
            }
        }
    }
}