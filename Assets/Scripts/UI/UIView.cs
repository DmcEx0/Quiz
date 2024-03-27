using Quiz.Level;
using Quiz.Tool;
using UnityEngine;
using Zenject;

namespace Quiz.UI
{
    public abstract class UIView : MonoBehaviour
    {
        [Space, Header("Fade Setting")]
        [SerializeField] private float _duration;
        [Range(0f, 1f)]
        [SerializeField] private float _fromValue;
        [Range(0f, 1f)]
        [SerializeField] private float _targetValue;

        private AnimationProvider _animationProvider;
        private LevelInitializer _levelInitializer;

        protected float Duration => _duration;
        protected float FromValue => _fromValue;
        protected float TargetValue => _targetValue;

        protected AnimationProvider AnimationProvider => _animationProvider;
        protected LevelInitializer LevelInitializer => _levelInitializer;

        [Inject]
        private void Construct(AnimationProvider animationProvider, LevelInitializer levelInitializer)
        {
            _animationProvider = animationProvider;
            _levelInitializer = levelInitializer;

            Init(animationProvider, levelInitializer);
        }

        protected virtual void Init(AnimationProvider animationProvider, LevelInitializer levelInitializer) { }
    }
}