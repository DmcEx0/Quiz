using UnityEngine;

namespace Quiz.Cell
{
    public class CellInteractionHandler : MonoBehaviour
    {
        private CellElement _cellElement;

        private void Awake()
        {
            _cellElement = GetComponent<CellElement>();
        }

        private void OnMouseDown()
        {
            if (_cellElement.IsClickabled)
                _cellElement.OnSelected();
        }
    }
}