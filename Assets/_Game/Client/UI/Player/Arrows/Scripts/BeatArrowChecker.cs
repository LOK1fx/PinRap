using System.Data;
using UnityEngine;

namespace LOK1game
{
    public class BeatArrowChecker : MonoBehaviour
    {
        [SerializeField] private float _destroyHeight;
        [SerializeField] private float _uiArrowBoundSize = 0.3f;
        
        private Transform _currentTargetUIArrowTransform;
        
        public void SetTargetUIArrow(Transform target)
        {
            _currentTargetUIArrowTransform = target;
        }

        private void LateUpdate()
        {
            if(transform.localPosition.y > _destroyHeight)
                Destroy(gameObject);
        }
        
        public bool IsInTargetUIArrowBound()
        {
            return Vector2.Distance(transform.position, _currentTargetUIArrowTransform.position) < _uiArrowBoundSize;
        }
    }
}