using Quiz.Cell;
using UnityEngine;

namespace Quiz.Factory
{
    public class CellFactory : GameObjectFactory
    {
        [SerializeField] private CellElement _cellElement;

        public CellElement Get()
        {
            GameObject gameObject = CreateInstance(_cellElement);
            CellElement cellElement = gameObject.GetComponent<CellElement>();

            return cellElement;
        }
    }
}