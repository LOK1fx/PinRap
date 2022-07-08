using UnityEngine;

namespace LOK1game.UI
{
    public class UIArrowSpawner : MonoBehaviour
    {
        [SerializeField] private BeatArrow _leftArrowPrefab;
        [SerializeField] private BeatArrow _downArrowPrefab;
        [SerializeField] private BeatArrow _upArrowPrefab;
        [SerializeField] private BeatArrow _rightArrowPrefab;

        [Space]
        [SerializeField] private Transform _arrowsSpawnPoint;
        [SerializeField] private Vector3 _arrowsSpaceOffset;

#if UNITY_EDITOR

        [SerializeField] private float _gizmoBoxScale = 50f;

#endif

        private EArrowType _currentArrowType;

        public void Spawn(EArrowType type)
        {
            if(type == EArrowType.None) { return; }

            _currentArrowType = type;

            switch (type)
            {
                case EArrowType.Left:
                    CreateArrow(_leftArrowPrefab);
                    break;
                case EArrowType.Down:
                    CreateArrow(_downArrowPrefab);
                    break;
                case EArrowType.Up:
                    CreateArrow(_upArrowPrefab);
                    break;
                case EArrowType.Right:
                    CreateArrow(_rightArrowPrefab);
                    break;
            }
        }

        private void CreateArrow(BeatArrow prefab)
        {
            var position = GetPositionForArrowType(_currentArrowType);
            var arrow = Instantiate(prefab, position, Quaternion.identity);

            position.y = 0;
            arrow.Setup(position);
        }

        private Vector3 GetPositionForArrowType(EArrowType type)
        {
            return _arrowsSpawnPoint.position + (int)type * Vector3.right + ((int)type * _arrowsSpaceOffset);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var scale = Vector3.one * _gizmoBoxScale;

            Gizmos.DrawWireCube(GetPositionForArrowType(EArrowType.Left), scale);
            Gizmos.DrawWireCube(GetPositionForArrowType(EArrowType.Down), scale);
            Gizmos.DrawWireCube(GetPositionForArrowType(EArrowType.Up), scale);
            Gizmos.DrawWireCube(GetPositionForArrowType(EArrowType.Right), scale);
        }

#endif
    }

    public enum EArrowType : int
    {
        None = -1,
        Left,
        Down,
        Up,
        Right
    }
}
