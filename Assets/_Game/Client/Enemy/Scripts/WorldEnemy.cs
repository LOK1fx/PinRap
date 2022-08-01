using UnityEngine;

namespace LOK1game.PinRap
{
    public class WorldEnemy : MonoBehaviour
    {
        public static WorldEnemy Instance { get; private set; }
        
        public PinRapEnemy EnemyPrefab => _enemyPrefab;
        
        [SerializeField] private PinRapEnemy _enemyPrefab;

        private void Awake()
        {
            Instance = this;
        }
    }
}