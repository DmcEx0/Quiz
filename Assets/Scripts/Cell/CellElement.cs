using Quiz.CardData;
using Quiz.Tool;
using System;
using UnityEngine;
using Zenject;

namespace Quiz.Cell
{
    public class CellElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _cardSpriteRenderer;
        [SerializeField] private Transform _cardTransform;
        [SerializeField] private GameObject _parcticle;

        [Space, Header("Enable")]
        [SerializeField] private AnimationCurve _enableAnimation;
        [SerializeField] private float _durationEnableAnimation;

        [Space, Header("Incorrect answer")]
        [SerializeField] private AnimationCurve _incorrectAnswerAnimation;
        [SerializeField] private float _durationIncorrectAnswerAnimation;

        private AnimationProvider _animationProvider;
        private CellFiller _cellFiller;

        private bool _isDisable;
        private string _identifier;

        public SpriteRenderer CardSpriteRenderer => _cardSpriteRenderer;
        public bool IsClickabled { get; private set; }
        public bool IsCorrectAnswer { get; private set; }
        public string Identifier => _identifier;

        public event Action SelectedCorrectAnswer;

        [Inject]
        private void Construct(AnimationProvider animationProvider, CellFiller cellFiller)
        {
            _animationProvider = animationProvider;
            _cellFiller = cellFiller;
        }

        public void Enable(bool isInitialized)
        {
            IsClickabled = true;
            _isDisable = true;

            _cardSpriteRenderer.transform.rotation = Quaternion.identity;

            if (isInitialized == false)
                _animationProvider.CallBounceEffect(transform, _durationEnableAnimation, _enableAnimation, SetAnimationFinishedStatus);
        }

        public void Disable()
        {
            IsCorrectAnswer = false;
            IsClickabled = false;
            _isDisable = false;

            _parcticle.gameObject.SetActive(false);
        }

        public void ResetCellFiller()
        {
            _cellFiller.Reset();
        }

        public void Renderer(CardPackData cardPackData)
        {
            _cellFiller.Render(cardPackData, IsCorrectAnswer, _cardSpriteRenderer, ref _identifier);
        }

        public void OnSelected()
        {
            IsClickabled = false;

            if (IsCorrectAnswer == true)
            {
                _animationProvider.CallBounceEffect(_cardTransform, _durationEnableAnimation, _enableAnimation, SetAnimationFinishedStatus);
                SelectedCorrectAnswer?.Invoke();
                _parcticle.gameObject.SetActive(true);
            }
            else
                _animationProvider.CallInBounceEffect(_cardTransform, _durationIncorrectAnswerAnimation, _incorrectAnswerAnimation, SetAnimationFinishedStatus);
        }

        public void SetTrueCorrectAnswer()
        {
            IsCorrectAnswer = true;
        }

        private void SetAnimationFinishedStatus(bool isFinished)
        {
            if (_isDisable == false)
                return;

            IsClickabled = isFinished;
        }
    }
}