using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Config.Level
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Config/Level")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private List<LevelData> _levelsData;

        public IReadOnlyList<LevelData> LevelsData => _levelsData;
    }
}