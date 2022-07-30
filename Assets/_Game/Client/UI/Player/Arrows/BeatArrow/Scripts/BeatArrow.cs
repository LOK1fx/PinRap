using System;
using LOK1game.Game;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        private const float MOVE_SPEED_MULTIPLIER = 10f; //Просто для удобства, чтобы не приходилось указывать огромные числа в редакторе

        public event Action<BeatArrow> OnDestroy;
        
        public BeatArrowChecker Observer { get; private set; }
        public EBeatEffectStrength BeatEffectStrength => _beatEffectStrength;
        public EArrowType Type { get; private set; }
        
        [SerializeField] private float _destroyHeight;
        [SerializeField] private float _moveSpeed;
        
        private EBeatEffectStrength _beatEffectStrength = EBeatEffectStrength.Weak;
        private bool _isPaused;

        private void Awake()
        {
            App.ProjectContext.GameStateManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(EGameState newGameState)
        {
            if (newGameState == EGameState.Paused)
            {
                _isPaused = true;
            }
            else
            {
                _isPaused = false;
            }
        }

        private void Update()
        {
            if(_isPaused) { return; }
            
            transform.localPosition += Vector3.up * (_moveSpeed * MOVE_SPEED_MULTIPLIER * Time.deltaTime);
        }
        
        private void LateUpdate()
        {
            if (transform.localPosition.y > _destroyHeight)
            {
                OnDestroy?.Invoke(this);
                
                Destroy(gameObject);
            }
        }

        public void Beat()
        {
            OnDestroy?.Invoke(this);
            
            Destroy(gameObject);
        }

        public void SetObserver(BeatArrowChecker checker)
        {
            Observer = checker;
        }

        public void Setup(EBeatEffectStrength effectStrength, EArrowType type, float moveSpeed)
        {
            _beatEffectStrength = effectStrength;
            _moveSpeed = moveSpeed;
            Type = type;
        }
    }
}