using System;
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
        public Transform leftArrowUITransform;
        public Transform rightArrowUITransform;
        public Transform upArrowUITransform;
        public Transform downArrowUITransform;
        public BeatArrow leftArrowPrefab;
        public BeatArrow downArrowPrefab;
        public BeatArrow upArrowPrefab;
        public BeatArrow rightArrowPrefab;

        [Space]
        public Transform arrowsSpawnPoint;
        public Transform arrowsDestroyPoint;

        public void Spawn(EArrowType type)
        {
            if (type == EArrowType.None) return;
            var uiArrowTransform = GetUIArrow(type);
            var nextPosition = new Vector3(uiArrowTransform.position.x, arrowsSpawnPoint.position.y,
                uiArrowTransform.position.z);

            switch (type)
            {
                case EArrowType.Left:
                    CreateArrow(leftArrowPrefab, nextPosition, uiArrowTransform);
                    break;
                case EArrowType.Down:
                    CreateArrow(downArrowPrefab, nextPosition, uiArrowTransform);
                    break;
                case EArrowType.Up:
                    CreateArrow(upArrowPrefab, nextPosition, uiArrowTransform);
                    break;
                case EArrowType.Right:
                    CreateArrow(rightArrowPrefab, nextPosition, uiArrowTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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
    }
}
