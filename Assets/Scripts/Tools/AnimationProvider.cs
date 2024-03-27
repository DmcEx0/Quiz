using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Tool
{
    public class AnimationProvider
    {
        private const bool IsAnimationFinish = true;

        public void CallFaidInEffect(Image image, float duration, float fromValue, float targetValue, Action<bool> animationFinishAction)
        {
            image.DOFade(targetValue, duration).From(fromValue)
                .SetAutoKill()
                .OnComplete(() => animationFinishAction(IsAnimationFinish));
        }

        public void CallFaidInEffect(Image image, float duration, float fromValue, float targetValue)
        {
            image.DOFade(targetValue, duration).From(fromValue)
                .SetAutoKill();
        }

        public void CallFaidInEffect(TMP_Text text, float duration, float fromValue, float targetValue)
        {
            text.DOFade(targetValue, duration).From(fromValue).SetAutoKill();
        }

        public void CallBounceEffect(Transform transform, float duration, AnimationCurve animationCurve, Action<bool> animationFinishAction)
        {
            transform.DOScale(transform.localScale.x + 0.1f, duration)
                      .SetEase(animationCurve)
                        .SetAutoKill().OnComplete(() => animationFinishAction(IsAnimationFinish));
        }

        public void CallInBounceEffect(Transform transform, float duration, AnimationCurve animationCurve, Action<bool> animationFinishAction)
        {
            transform.DOMoveX(transform.position.x + 0.1f, duration)
                    .SetEase(animationCurve)
                    .SetAutoKill().OnComplete(() => animationFinishAction(IsAnimationFinish));
        }
    }
}