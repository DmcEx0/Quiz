using Quiz.Level;
using Quiz.Tool;
using TMPro;
using UnityEngine;

namespace Quiz.UI
{
    public class FindingTextElementUI : UIView, IInitializeble
    {
        [SerializeField] private string _prefixText;
        [SerializeField] private TMP_Text _text;

        public void ChangeFindingSign(string sign)
        {
            _text.text = string.Format(_prefixText, sign);
        }

        public void Initialize()
        {
            AnimationProvider.CallFaidInEffect(_text, Duration, FromValue, TargetValue);
        }

        public void Shutdown()
        {
            _text.alpha = 0f;
        }

        protected override void Init(AnimationProvider animationProvider, LevelInitializer levelInitializer)
        {
            LevelInitializer.Add(this);
        }
    }
}