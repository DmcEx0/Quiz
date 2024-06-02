using TMPro;
using UnityEngine;

namespace Quiz.View.UI
{
    public class FindingTextView : MonoBehaviour
    {
        [SerializeField]
        private string _prefixText;

        [SerializeField]
        private TMP_Text _text;

        public TMP_Text Text => _text;

        public void ChangeFindingSign(string sign)
        {
            _text.text = string.Format(_prefixText, sign);
        }
    }
}