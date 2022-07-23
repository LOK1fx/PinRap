using UnityEngine;
using UnityEngine.Serialization;

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
        //FormerlySerializedAs attribute added to prevent links missed
        [FormerlySerializedAs("leftArrowUITransform")] [SerializeField] private Transform _leftArrowUITransform;
        [FormerlySerializedAs("rightArrowUITransform")] [SerializeField] private Transform _rightArrowUITransform;
        [FormerlySerializedAs("upArrowUITransform")] [SerializeField] private Transform _upArrowUITransform;
        [FormerlySerializedAs("downArrowUITransform")] [SerializeField] private Transform _downArrowUITransform;
        [FormerlySerializedAs("leftArrowPrefab")] [SerializeField] private BeatArrow _leftArrowPrefab;
        [FormerlySerializedAs("downArrowPrefab")] [SerializeField] private BeatArrow _downArrowPrefab;
        [FormerlySerializedAs("upArrowPrefab")] [SerializeField] private BeatArrow _upArrowPrefab;
        [FormerlySerializedAs("rightArrowPrefab")] [SerializeField] private BeatArrow _rightArrowPrefab;

        [Space]
        [SerializeField] private Transform arrowsSpawnPoint;

        public void Spawn(EArrowType type)
        {
            if (type == EArrowType.None) return;
            
            var uiArrowTransform = GetUIArrowTransform(type);
            var arrowPrefab = GetArrowPrefab(type);
            var nextPosition = new Vector3(uiArrowTransform.position.x, arrowsSpawnPoint.position.y,
                uiArrowTransform.position.z); 
            
            CreateArrow(arrowPrefab, nextPosition, uiArrowTransform);
        }

        private void CreateArrow(BeatArrow prefab, Vector3 spawnPosition, Transform targetUIArrow)
        {
            var arrow = Instantiate(prefab, transform);
            arrow.transform.position = spawnPosition;
            arrow.Checker.SetTargetUIArrow(targetUIArrow);
        }

        private BeatArrow GetArrowPrefab(EArrowType type)
        {
            return type switch
            {
                EArrowType.Down => _downArrowPrefab,
                EArrowType.Up => _upArrowPrefab,
                EArrowType.Right => _rightArrowPrefab,
                EArrowType.Left => _leftArrowPrefab,
                _ => null
            };
        }
        
        private Transform GetUIArrowTransform(EArrowType type)
        {
            return type switch
            {
                EArrowType.Down => _downArrowUITransform,
                EArrowType.Up => _upArrowUITransform,
                EArrowType.Right => _rightArrowUITransform,
                EArrowType.Left => _leftArrowUITransform,
                _ => null
            };
        }
    }
}
