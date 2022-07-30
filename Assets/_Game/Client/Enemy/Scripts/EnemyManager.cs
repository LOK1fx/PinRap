using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LOK1game
{
    public class EnemyManager
    {
        private readonly List<PinRapEnemy> _enemies = new List<PinRapEnemy>();

        public void CreateEnemy(PinRapEnemy prefab)
        {
            var enemy = Object.Instantiate(prefab, GetRandomSpawnPosition(true), Quaternion.identity);
            
            Controller.Create<PinRapEnemyController>(enemy);
            
            _enemies.Add(enemy);
        }

        public IEnumerator DestroyAllEnemies()
        {
            foreach (var enemy in _enemies)
            {
                _enemies.Remove(enemy);
                
                Object.Destroy(enemy.gameObject);

                yield return new WaitForEndOfFrame();
            }
        }

        private static Vector3 GetRandomSpawnPosition(bool enemyFlag)
        {
            var spawnPoints = Object.FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (enemyFlag)
                spawnPoints.RemoveAll(point => point.AllowEnemy == false);

            if (spawnPoints.Count < 1)
                return Vector3.zero;
   
            return spawnPoints[Random.Range(0, spawnPoints.Count)].Position;
        }
    }
}