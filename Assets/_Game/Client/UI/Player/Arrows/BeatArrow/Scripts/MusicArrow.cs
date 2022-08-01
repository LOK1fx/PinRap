using System;
using LOK1game.Game;
using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    public class MusicArrow : MonoBehaviour
    {
        private const float MOVE_SPEED_MULTIPLIER = 10f; //Просто для удобства, чтобы не приходилось указывать огромные числа в редакторе

        public delegate void ArrowDestroyed(MusicArrow arrow, bool missed);
        public event ArrowDestroyed OnDestroy;
        
        public MusicArrowChecker Observer { get; private set; }
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
                OnDestroy?.Invoke(this, true);
                
                //TODO: REWORK THIS SHIT!!!
                PlayerHud.Instance.DominationBar.RemovePoints(1);
                
                Destroy(gameObject);
            }
        }

        public void Beat()
        {
            OnDestroy?.Invoke(this, false);
            
            Destroy(gameObject);
        }

        public void SetObserver(MusicArrowChecker checker)
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