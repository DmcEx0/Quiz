using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Config.Animation
{
    [CreateAssetMenu(fileName = "GeneralAnimationsConfig", menuName = "Config/Animation")]
    public class AnimationsConfig : ScriptableObject
    {
        [SerializeField]
        private List<AnimationData> _animationsData;

        public AnimationData GetAnimationData(AnimationsType animationsType)
        {
            AnimationData currentData = _animationsData[0];

            foreach (var animationData in _animationsData)
            {
                if (animationsType == animationData.Type)
                    currentData = animationData;
            }

            return currentData;
        }
    }
}