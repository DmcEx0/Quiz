using System.Collections.Generic;
using UnityEngine;

namespace Quiz.CardData
{
    [CreateAssetMenu(fileName = "NewCardPack", menuName = "Card/NewPack")]
    public class CardPackData : ScriptableObject
    {
        [SerializeField] private List<Card> _cards;

        public IReadOnlyCollection<Card> Cards => _cards;
    }
}