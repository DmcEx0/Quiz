using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Quiz.Factory
{
    public class AssetsFactory
    {
        private AsyncOperationHandle<GameObject> _loadOp;

        public async UniTask<GameObject> Get(AssetReference prefab, CancellationToken token)
        {
            _loadOp = Addressables.InstantiateAsync(prefab);

            var cellView = await _loadOp.WithCancellation(token);

            return cellView;
        }

        public void Release(GameObject prefab)
        {
            Addressables.ReleaseInstance(prefab);
        }
    }
}