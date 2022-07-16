using UnityEngine;

namespace LOK1game.UI
{
    public enum EArrowType
    {
        None = -1,
        Left,
        Down,
        Up,
        Right
    }
    
    public class UIArrowSpawner : MonoBehaviour
    {
        [SerializeField] private Transform leftArrowUITransform;
        [SerializeField] private Transform rightArrowUITransform;
        [SerializeField] private Transform upArrowUITransform;
        [SerializeField] private Transform downArrowUITransform;
        [SerializeField] private BeatArrow leftArrowPrefab;
        [SerializeField] private BeatArrow downArrowPrefab;
        [SerializeField] private BeatArrow upArrowPrefab;
        [SerializeField] private BeatArrow rightArrowPrefab;

        [Space]
        [SerializeField] private Transform arrowsSpawnPoint;
        [SerializeField] private Transform arrowsDestroyPoint;

        public void Spawn(EArrowType type)
        {
            if (type == EArrowType.None) return;
            var uiArrowTransform = GetUIArrowAndPrefab(type, out var arrowPrefab);
            var nextPosition = new Vector3(uiArrowTransform.position.x, arrowsSpawnPoint.position.y,
                uiArrowTransform.position.z); 
            CreateArrow(arrowPrefab, nextPosition, uiArrowTransform);
        }

        private void CreateArrow(BeatArrow prefab, Vector3 spawnPosition, Transform targetUIArrow)
        {
            var arrow = Instantiate(prefab, transform);
            arrow.transform.position = spawnPosition;
            var component = arrow.GetComponent<BeatArrowChecker>();
            component.DestroyPosition = arrowsDestroyPoint.position;
            component.CurrentUIArrow = targetUIArrow;
        }

        private Transform GetUIArrow(EArrowType type)
        {
            return type switch
            {
                EArrowType.Down => downArrowUITransform,
                EArrowType.Up => upArrowUITransform,
                EArrowType.Right => rightArrowUITransform,
                EArrowType.Left => leftArrowUITransform,
                _ => null
            };
        }
        
        private Transform GetUIArrowAndPrefab(EArrowType type, out BeatArrow arrowPrefab)
        {
            switch (type)
            {
                case EArrowType.Down:
                    arrowPrefab = downArrowPrefab;
                    return downArrowUITransform;
                case EArrowType.Up:
                    arrowPrefab = upArrowPrefab;
                    return upArrowUITransform;
                case EArrowType.Right:
                    arrowPrefab = rightArrowPrefab;
                    return rightArrowUITransform;
                case EArrowType.Left:
                    arrowPrefab = leftArrowPrefab;
                    return leftArrowUITransform;
                default:
                    arrowPrefab = null;
                    return null;
            }
        }
    }
}
