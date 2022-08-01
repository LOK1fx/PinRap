﻿using System;
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
        public event Action<MusicArrow> ArrowSpawned;

        public MusicArrowChecker LeftArrowChecker => _leftArrowChecker;
        public MusicArrowChecker UpArrowChecker => _upArrowChecker;
        public MusicArrowChecker DownArrowChecker => _downArrowChecker;
        public MusicArrowChecker RightArrowChecker => _rightArrowChecker;
        
        //FormerlySerializedAs attribute added to prevent links missed
        [FormerlySerializedAs("leftArrowUITransform")] [SerializeField] private Transform _leftArrowUITransform;
        [FormerlySerializedAs("rightArrowUITransform")] [SerializeField] private Transform _rightArrowUITransform;
        [FormerlySerializedAs("upArrowUITransform")] [SerializeField] private Transform _upArrowUITransform;
        [FormerlySerializedAs("downArrowUITransform")] [SerializeField] private Transform _downArrowUITransform;
        
        [FormerlySerializedAs("leftArrowPrefab")] [SerializeField] private MusicArrow _leftArrowPrefab;
        [FormerlySerializedAs("downArrowPrefab")] [SerializeField] private MusicArrow _downArrowPrefab;
        [FormerlySerializedAs("upArrowPrefab")] [SerializeField] private MusicArrow _upArrowPrefab;
        [FormerlySerializedAs("rightArrowPrefab")] [SerializeField] private MusicArrow _rightArrowPrefab;

        [Space]
        [SerializeField] private MusicArrowChecker _leftArrowChecker;
        [SerializeField] private MusicArrowChecker _upArrowChecker;
        [SerializeField] private MusicArrowChecker _downArrowChecker;
        [SerializeField] private MusicArrowChecker _rightArrowChecker;

        [Space]
        [SerializeField] private Transform arrowsSpawnPoint;

        public void Spawn(ArrowData data)
        {
            Spawn(data.Type, data.Speed, data.Strength);
        }
        
        public void Spawn(EArrowType type, float moveSpeed = 20f, EBeatEffectStrength beatEffectStrength = EBeatEffectStrength.None)
        {
            if (type == EArrowType.None) return;
            
            var uiArrowTransform = GetUIArrowTransform(type);
            var arrowPrefab = GetArrowPrefab(type);
            var nextPosition = new Vector3(uiArrowTransform.position.x, arrowsSpawnPoint.position.y,
                uiArrowTransform.position.z); 
            
            CreateArrow(arrowPrefab, nextPosition, GetChecker(type), beatEffectStrength, type, moveSpeed);
        }

        //TODO: Rework this shit
        private void CreateArrow(MusicArrow prefab, Vector3 spawnPosition, MusicArrowChecker checker,
            EBeatEffectStrength strength, EArrowType type, float moveSpeed)
        {
            var arrow = Instantiate(prefab, transform);
            
            arrow.transform.position = spawnPosition;
            arrow.Setup(strength, type, moveSpeed);
            arrow.SetObserver(checker);
            arrow.OnDestroy += ArrowOnDestroyed;
            
            checker.AddArrowToVision(arrow);
            
            ArrowSpawned?.Invoke(arrow);
        }

        private void ArrowOnDestroyed(MusicArrow arrow, bool missed)
        {
            arrow.OnDestroy -= ArrowOnDestroyed;
            arrow.Observer.RemoveArrowFromVision(arrow);
        }

        private MusicArrow GetArrowPrefab(EArrowType type)
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

        private MusicArrowChecker GetChecker(EArrowType type)
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
