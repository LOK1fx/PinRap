using UnityEngine;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool _showDebugPaths;

        [Space]
#endif

        [SerializeField] private LayerMask _hitableLayer;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction;
        [SerializeField] private float _lifeTime = 4f;

        private Rigidbody _rigidbody;
        private Damage _damage;

        private Vector3 _previusPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Destroy(gameObject, _lifeTime);
        }

        public void Shoot(Vector3 direction, float force, Damage damage)
        {
            _previusPosition = transform.position;
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            _damage = damage;
        }

        private void LateUpdate()
        {
            //�������� �� ����� ������� ������ �������.
            //���� �������� ������� ������� �������, �� ����� ������ ��������� ���� ���.
            //�������� ������� � ������� ������� � ������� ����� � �������.
            if(Physics.Linecast(_previusPosition, transform.position, out var hit, _hitableLayer, _triggerInteraction))
            {
                Impact(hit);

                Destroy(gameObject);

                return;
            }

#if UNITY_EDITOR

            if(_showDebugPaths)
                Debug.DrawLine(_previusPosition, transform.position, Color.yellow, 1f);

#endif

            _previusPosition = transform.position;
        }

        private void Impact(RaycastHit hit)
        {
            _damage.HitNormal = hit.normal;
            _damage.HitPoint = hit.point;

            var gameObject = hit.collider;

            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                var damagables = gameObject.GetComponents<IDamagable>();

                foreach (var damage in damagables)
                    damage.TakeDamage(_damage);
            }
            if(gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                if(gameObject.TryGetComponent<Actor>(out var actor))
                    return;

                rb.AddForceAtPosition((_rigidbody.velocity * 0.001f) * _damage.Value, hit.point, ForceMode.Impulse);
            }
        }
    }
}