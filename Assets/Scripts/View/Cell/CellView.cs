using Quiz.Tool;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.View.Cell
{
    public class CellView : MonoBehaviour, IClickable
    {
        [SerializeField]
        private BoxCollider2D _cardCollider;
        [SerializeField]
        private SpriteRenderer _cardSpriteRenderer;
        [SerializeField]
        private SpriteRenderer _cellSpriteRenderer;
        [SerializeField]
        private Transform _cardTransform;
        [SerializeField]
        private ParticleSystem _parcticle;

        private string _identifier;
        public BoxCollider2D CardCollider => _cardCollider;
        public SpriteRenderer CardSpriteRenderer => _cardSpriteRenderer;
        public SpriteRenderer CellSpriteRenderer => _cellSpriteRenderer;
        public Transform CardTransform => _cardTransform;
        public ParticleSystem Particle => _parcticle;
        public string Identifier => _identifier;

        public event Action<CellView> Clicked;

        public void SetIdentifier(string identifier)
        {
            _identifier = identifier;
        }

        public void Click()
        {
            Clicked?.Invoke(this);
        }
    }
}