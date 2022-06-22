using LOK1game.Game.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LOK1game.Test
{
    [RequireComponent(typeof(Health))]
    public class TestEnemy : Actor, IDamagable
    {
        [SerializeField] private bool _debug_test_is_agent;
        [SerializeField] private Transform _debug_test_target;
        private NavMeshAgent _debug_test_agent;

        [Space]
        [SerializeField] private float _resetHurtTime = 1f;
        [SerializeField] private Material _hurtedMaterial;
        [SerializeField] private Renderer[] _meshes;

        private List<Material> _defaultMaterials = new List<Material>();
        private Health _health;

        private float _currentResetMatTimer;

        private void Start()
        {
            _health = GetComponent<Health>();

            foreach (var mesh in _meshes)
            {
                _defaultMaterials.Add(mesh.sharedMaterial);
            }

            if(_debug_test_is_agent)
            {
                _debug_test_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void OnValidate()
        {
            if(_debug_test_is_agent && _debug_test_agent == null)
            {
                _debug_test_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Update()
        {
            if(_currentResetMatTimer > 0)
            {
                _currentResetMatTimer -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < _meshes.Length; i++)
                {
                    _meshes[i].sharedMaterial = _defaultMaterials[i];
                }
            }

            if(_debug_test_is_agent && _debug_test_agent != null)
            {
                _debug_test_agent.SetDestination(_debug_test_target.position);
            }
        }

        public void TakeDamage(Damage damage)
        {
            _health.ReduceHealth(damage.Value);

            Debug.Log($"{gameObject.name}: Take the hit - {damage.Value}d");

            var evt = Events.OnPlayerHit;

            evt.PlayerId = 999;
            evt.Damage = damage.Value;
            evt.HitPosition = damage.HitPoint;

            EventManager.Broadcast(evt);

            SetMeshesMaterial(_hurtedMaterial);
            _currentResetMatTimer = _resetHurtTime;
        }

        private void SetMeshesMaterial(Material material)
        {
            foreach (var mesh in _meshes)
            {
                mesh.sharedMaterial = material;
            }
        }
    }
}