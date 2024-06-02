using Cysharp.Threading.Tasks;
using Quiz.Factory;
using Quiz.View.Cell;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CellViewPool
{
    private readonly AssetsFactory _cellFactory;
    private readonly List<GameObject> _views;
    private readonly AssetReference _cellPrefab;

    private int _index = 0;

    public CellViewPool(AssetReference cellPrefab, AssetsFactory cellFactory)
    {
        _cellFactory = cellFactory;
        _views = new List<GameObject>();

        _cellPrefab = cellPrefab;
    }

    public async UniTask Load(int count, CancellationToken token)
    {
        for (int i = 0; i < count; i++)
        {
            var newCell = await _cellFactory.Get(_cellPrefab, token);
            newCell.gameObject.SetActive(false);

            _views.Add(newCell);
        }
    }

    public void Unload()
    {
        foreach (var view in _views)
        {
            _cellFactory.Release(view);
        }

        _index = 0;
        _views.Clear();
    }

    public CellView Get()
    {
        return _views[_index++].GetComponent<CellView>();
    }
}