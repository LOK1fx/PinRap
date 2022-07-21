using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game.World
{
    [RequireComponent(typeof(Health))]
    public class FarmCrystal : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _farmScoreMultiplier = 1f;

        [Space]
        [SerializeField] private GameObject _hitEffectPrefab;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public void TakeDamage(Damage damage)
        {
            if(damage.DamageType != EDamageType.Drill) { return; }

            if(damage.Sender is Player.Player)
            {
                _health.ReduceHealth(damage.Value);

                var score = Mathf.RoundToInt(damage.Value * _farmScoreMultiplier);

                Debug.Log($"Crystal farm. Farm score - {score}");
            }

            var effect = Instantiate(_hitEffectPrefab, damage.HitPoint, Quaternion.identity);

            effect.transform.LookAt(damage.HitPoint + damage.HitNormal);

            Destroy(effect, 1f);

            if(_health.Hp <= 0)
            {
                DestroyCrystal();
            }
        }

        private void DestroyCrystal()
        {
            Destroy(gameObject);
        }
    }
}