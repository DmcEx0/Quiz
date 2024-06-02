using System;
using UnityEngine;

namespace Quiz.Config.Animation
{
    [Serializable]
    public class AnimationData
    {
        [SerializeField]
        private AnimationsType _type;

        [Space]
        [SerializeField]
        private AnimationCurve _animationCurve;

        [SerializeField]
        private float _duration;

        public AnimationsType Type => _type;
        public AnimationCurve AnimationCurve => _animationCurve;
        public float Duration => _duration;
    }
}