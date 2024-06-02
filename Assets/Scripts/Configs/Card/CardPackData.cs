using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Config.Card
{
    [CreateAssetMenu(fileName = "NewCardPackConfig", menuName = "Config/NewPack")]
    public class CardPackData : ScriptableObject
    {
        [SerializeField]
        private List<CardElementData> _cards;

        public IReadOnlyCollection<CardElementData> Cards => _cards;
    }
}