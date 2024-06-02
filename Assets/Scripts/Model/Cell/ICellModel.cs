using Quiz.Config.Card;
using Quiz.View.Cell;

namespace Quiz.Model.Cell
{
    public interface ICellModel
    {
        public void Initialize();

        public void StartNextIteration(CardPackData cardPackData, int requiredAmount);

        public CellView GetNewCellForPlace(int posX, int posY);

        public string GetCurrentCorrectAnswer();
    }
}