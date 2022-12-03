using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu(fileName = "new LevelConfigData", menuName = "Level/Config")]
    public class LevelConfigData : ScriptableObject
    {
        public int StartPlayerPoints => _startPlayerPoints;
        public int LosedPoints => _losedPoints;
        public int EarnPoints => _earnPoints;

        [SerializeField] private int _startPlayerPoints = 50;
        [SerializeField] private int _losedPoints = 5;
        [SerializeField] private int _earnPoints = 1;
    }
}