using UnityEngine;
using Zenject;

namespace Quiz.Factory
{
    public abstract class GameObjectFactory : MonoBehaviour
    {
        [Inject]
        private readonly DiContainer _container;

        protected GameObject CreateInstance<T>(T prefab) where T : MonoBehaviour
        {
            var instance = _container.InstantiatePrefab(prefab);

            return instance;
        }
    }
}