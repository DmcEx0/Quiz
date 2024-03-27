using Quiz.Level;
using UnityEngine;

namespace Quiz.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private LevelProvider _levelSelector;
        [SerializeField] private EndGameWindowView _view;

        private void OnEnable()
        {
            _levelSelector.LevelsOver += Open;
        }

        private void OnDisable()
        {
            _levelSelector.LevelsOver -= Open;
        }

        private void Open()
        {
            _view.gameObject.SetActive(true);
        }
    }
}