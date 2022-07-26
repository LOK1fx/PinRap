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
        public BeatArrowChecker LeftArrowChecker => _leftArrowChecker;
        public BeatArrowChecker UpArrowChecker => _upArrowChecker;
        public BeatArrowChecker DownArrowChecker => _downArrowChecker;
        public BeatArrowChecker RightArrowChecker => _rightArrowChecker;
        
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
        [SerializeField] private BeatArrowChecker _leftArrowChecker;
        [SerializeField] private BeatArrowChecker _upArrowChecker;
        [SerializeField] private BeatArrowChecker _downArrowChecker;
        [SerializeField] private BeatArrowChecker _rightArrowChecker;

        [Space]
        [SerializeField] private Transform arrowsSpawnPoint;

        public void Spawn(EArrowType type, EBeatEffectStrength beatEffectStrength = EBeatEffectStrength.None)
        {
            if (type == EArrowType.None) return;
            
            var uiArrowTransform = GetUIArrowTransform(type);
            var arrowPrefab = GetArrowPrefab(type);
            var nextPosition = new Vector3(uiArrowTransform.position.x, arrowsSpawnPoint.position.y,
                uiArrowTransform.position.z); 
            
            CreateArrow(arrowPrefab, nextPosition, GetChecker(type), beatEffectStrength);
        }

        private void CreateArrow(BeatArrow prefab, Vector3 spawnPosition, BeatArrowChecker checker, EBeatEffectStrength strength)
        {
            var arrow = Instantiate(prefab, transform);
            
            arrow.transform.position = spawnPosition;
            arrow.Setup(strength);
            arrow.SetObserver(checker);
            arrow.OnDestroy += ArrowOnDestroyed;
            
            checker.AddArrowToVision(arrow);
        }

        private void ArrowOnDestroyed(BeatArrow arrow)
        {
            arrow.OnDestroy -= ArrowOnDestroyed;
            arrow.Observer.RemoveArrowFromVision(arrow);
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

        private BeatArrowChecker GetChecker(EArrowType type)
        {
            return type switch
            {
                EArrowType.Down => _downArrowChecker,
                EArrowType.Up => _upArrowChecker,
                EArrowType.Right => _rightArrowChecker,
                EArrowType.Left => _leftArrowChecker,
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
