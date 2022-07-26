using System;
using UnityEngine;

namespace LOK1game
{
    public class BeatArrow : MonoBehaviour
    {
        private const float MOVE_SPEED_MULTIPLIER = 10f; //Просто для удобства, чтобы не приходилось указывать огромные числа в редакторе

        public event Action<BeatArrow> OnDestroy;
        
        public BeatArrowChecker Observer { get; private set; }
        
        [SerializeField] private float _destroyHeight;
        [SerializeField] private float _moveSpeed;
        
        private EBeatEffectStrength _beatEffectStrength = EBeatEffectStrength.Weak;

        private void Update()
        {
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
            if(_beatEffectStrength != EBeatEffectStrength.None)
                ClientApp.ClientContext.BeatController.InstantiateBeat(_beatEffectStrength);
            
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        }

        public void SetObserver(BeatArrowChecker checker)
        {
            Observer = checker;
        }

        public void Setup(EBeatEffectStrength effectStrength)
        {
            _beatEffectStrength = effectStrength;
        }
    }
}