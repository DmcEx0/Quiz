using Quiz.Tool;
using System.Collections.Generic;

namespace Quiz.Level
{
    public class LevelInitializer
    {
        private List<IInitializeble> _initializebles;

        public LevelInitializer()
        {
            _initializebles = new List<IInitializeble>();
        }

        public void Add(IInitializeble initializebles)
        {
            _initializebles.Add(initializebles);
        }

        public void InitializeAll()
        {
            foreach (var init in _initializebles)
            {
                init.Initialize();
            }
        }

        public void ShutdownAll()
        {
            foreach (var init in _initializebles)
            {
                init.Shutdown();
            }
        }
    }
}