using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private AssetReference _mainMenuScene;

    private void Start()
    {
        LoadSceneAsync(CancellationToken.None).Forget();
    }

    private async UniTask LoadSceneAsync(CancellationToken token)
    {
        await Addressables.LoadSceneAsync(_mainMenuScene).WithCancellation(token);
    }
}