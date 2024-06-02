using Cysharp.Threading.Tasks;
using Quiz.Config.Animation;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Tool
{
    public class AsyncAnimationProvider
    {
        private readonly AnimationsConfig _animationsConfig;

        public AsyncAnimationProvider(AnimationsConfig animationsConfig)
        {
            _animationsConfig = animationsConfig;
        }

        public async UniTask CallFadeInEffectImageAsync(Image image, AnimationsType type, CancellationToken token)
        {
            var currentData = _animationsConfig.GetAnimationData(type);

            await image.DOFade(GetTargetAndFromValues(currentData).Item1, currentData.Duration).From(GetTargetAndFromValues(currentData).Item2)
                 .AwaitForComplete(TweenCancelBehaviour.Complete, token);
        }

        public async UniTask CallFadeInEffectTextAsync(TMP_Text text, AnimationsType type, CancellationToken token)
        {
            var currentData = _animationsConfig.GetAnimationData(type);

            await text.DOFade(GetTargetAndFromValues(currentData).Item1, currentData.Duration).From(GetTargetAndFromValues(currentData).Item2)
                .AwaitForComplete(TweenCancelBehaviour.Complete, token);
        }

        public async UniTask CallBounceEffectAsync(Transform transform, AnimationsType type, CancellationToken token)
        {
            var currentData = _animationsConfig.GetAnimationData(type);

            await transform.DOScale(transform.localScale.x + 0.1f, currentData.Duration)
                      .SetEase(currentData.AnimationCurve)
                      .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        public async UniTask CallInBounceEffectAsync(Transform transform, AnimationsType type, CancellationToken token)
        {
            var currentData = _animationsConfig.GetAnimationData(type);

            await transform.DOMoveX(transform.position.x + 0.1f, currentData.Duration)
                     .SetEase(currentData.AnimationCurve)
                     .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        private (float, float) GetTargetAndFromValues(AnimationData currentData)
        {
            int keysAmount = currentData.AnimationCurve.keys.Length;

            float targetValue = currentData.AnimationCurve.keys[keysAmount - 1].value;
            float fromValue = currentData.AnimationCurve.keys[0].value;

            return (targetValue, fromValue);
        }
    }
}