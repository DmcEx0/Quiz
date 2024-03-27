using Quiz.CardData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz.Cell
{
    public class CellFiller
    {
        private const int AngleToRotate = -90;

        private readonly List<Card> _allCards;
        private readonly List<Card> _commonCardsUsedAsCorrectAnswer;

        private List<Card> _currentListCard;

        private Card _cardToCorrectAnswer;
        private int _currentSpriteIndex;

        public CellFiller()
        {
            _allCards = new List<Card>();
            _commonCardsUsedAsCorrectAnswer = new List<Card>();
        }

        public void Render(CardPackData cardPackData, bool isCorrectAnswer, SpriteRenderer spriteRenderer, ref string identifier)
        {
            _currentListCard = new List<Card>(cardPackData.Cards);

            if (_commonCardsUsedAsCorrectAnswer.Count == _currentListCard.Count - 1)
            {
                _commonCardsUsedAsCorrectAnswer.Clear();
            }

            if (_allCards.Count == 0)
            {
                var notUseElement = _currentListCard.Except(_commonCardsUsedAsCorrectAnswer);

                List<Card> tempList = new List<Card>(notUseElement);

                _cardToCorrectAnswer = tempList[Random.Range(0, tempList.Count)];
                tempList.Remove(_cardToCorrectAnswer);
                _commonCardsUsedAsCorrectAnswer.Add(_cardToCorrectAnswer);

                _allCards.AddRange(tempList);

                var missingElement = _currentListCard.Except(_allCards);

                _allCards.AddRange(missingElement);
                _allCards.Remove(_cardToCorrectAnswer);

                Shuffle(_allCards);

                _allCards.Add(_cardToCorrectAnswer);
            }

            if (isCorrectAnswer)
            {
                SetStrite(spriteRenderer, ref identifier, _cardToCorrectAnswer);
                _currentSpriteIndex--;
            }
            else
            {
                SetStrite(spriteRenderer, ref identifier, _allCards[_currentSpriteIndex]);
            }

            _currentSpriteIndex++;
        }

        public void Reset()
        {
            _allCards.Clear();
            _currentSpriteIndex = 0;
        }

        private void Shuffle(List<Card> cards)
        {
            int index = cards.Count;

            while (index > 1)
            {
                index--;

                int randomElement = Random.Range(0, index + 1);

                Card card = cards[randomElement];

                cards[randomElement] = cards[index];
                cards[index] = card;
            }
        }

        private void SetStrite(SpriteRenderer spriteRenderer, ref string identifier, Card card)
        {
            spriteRenderer.sprite = card.Sprite;
            identifier = card.Identifier;

            if (card.NeedRotate)
                spriteRenderer.transform.Rotate(0, 0, AngleToRotate);
        }
    }
}