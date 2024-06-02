using Cysharp.Threading.Tasks;
using Quiz.Config.Animation;
using Quiz.Config.Card;
using Quiz.Model.Cell;
using Quiz.Model.Level;
using Quiz.Tool;
using Quiz.View.Cell;
using Quiz.View.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;

namespace Quiz.Controllers
{
    public class CellsController : IInitializable, IDisposable
    {
        private readonly FindingTextView _findingTextView;

        private readonly ILevelModel _levelModel;
        private readonly ICellModel _cellModel;

        private readonly List<CellView> _cellsView;

        private readonly AsyncAnimationProvider _animationProvider;

        private CancellationTokenSource _tokenSource;

        public CellsController(ILevelModel levelModel, ICellModel cellModel, FindingTextView findingText,
            AsyncAnimationProvider animationProvider)
        {
            _cellModel = cellModel;
            _levelModel = levelModel;
            _findingTextView = findingText;
            _animationProvider = animationProvider;

            _cellsView = new List<CellView>();
        }

        public void Initialize()
        {
            _tokenSource = new CancellationTokenSource();

            _cellModel.Initialize();

            _levelModel.StartedNextIteration += StartNextIteration;
            _levelModel.CellPlacing += PlaceCell;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();

            _levelModel.StartedNextIteration -= StartNextIteration;
            _levelModel.CellPlacing -= PlaceCell;
        }


        private void StartNextIteration(CardPackData cardPackData, int size)
        {
            DisableViews();

            _findingTextView.Text.alpha = 1;

            _cellModel.StartNextIteration(cardPackData, size);

            SetFindingText();
        }

        private void PlaceCell(int posX, int posY, bool isFirstLevel)
        {
            var newCell = _cellModel.GetNewCellForPlace(posX, posY);

            AnimatingCell(newCell, isFirstLevel);

            FadeInText(isFirstLevel);

            newCell.Clicked += CellClick;

            _cellsView.Add(newCell);
        }

        private void CellClick(CellView cellView)
        {
            CellClickAsync().Forget();

            async UniTask CellClickAsync()
            {
                if (_cellModel.GetCurrentCorrectAnswer() == cellView.Identifier)
                {
                    ResetTokenSource();

                    DisableViews();
                    cellView.Particle.gameObject.SetActive(true);

                    await _animationProvider.CallBounceEffectAsync(cellView.CardTransform,
                        AnimationsType.CorrenctAnswer, _tokenSource.Token);

                    cellView.Particle.gameObject.SetActive(false);

                    _levelModel.SelectCorrectAnswer();
                }
                else
                {
                    ResetTokenSource();

                    await _animationProvider.CallInBounceEffectAsync(cellView.CardTransform,
                        AnimationsType.UncorrectAnswer, _tokenSource.Token);
                }
            }
        }

        private void AnimatingCell(CellView cellView, bool isInitialized)
        {
            if (isInitialized == false)
                return;

            AnimatingCellAsync().Forget();

            async UniTask AnimatingCellAsync()
            {
                await _animationProvider.CallBounceEffectAsync(cellView.transform, AnimationsType.PlacingCell,
                    _tokenSource.Token);
            }
        }

        private void FadeInText(bool isInitialized)
        {
            if (isInitialized == false)
                return;

            FadeInTextAsync().Forget();

            async UniTask FadeInTextAsync()
            {
                await _animationProvider.CallFadeInEffectTextAsync(_findingTextView.Text, AnimationsType.FaidInText,
                    _tokenSource.Token);
            }
        }

        private void SetFindingText()
        {
            string identifier = _cellModel.GetCurrentCorrectAnswer();

            _findingTextView.ChangeFindingSign(identifier);
        }

        private void DisableViews()
        {
            if (_cellsView.Count == 0)
            {
                return;
            }

            foreach (var cell in _cellsView)
            {
                cell.Clicked -= CellClick;
            }

            _cellsView.Clear();
        }

        private void ResetTokenSource()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }
    }
}