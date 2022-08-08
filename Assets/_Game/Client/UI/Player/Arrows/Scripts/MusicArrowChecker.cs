using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(MusicArrowCheckerVisual))]
    public class MusicArrowChecker : MonoBehaviour
    {
        public MusicArrowCheckerVisual Visual { get; private set; }
        
        [SerializeField] private float _uiArrowBoundSize = 0.3f;
        private readonly List<MusicArrow> _arrowsToCheck = new List<MusicArrow>();

        private void Awake()
        {
            Visual = GetComponent<MusicArrowCheckerVisual>();
        }

        public bool IsArrowInbound(out MusicArrow boundArrow)
        {
            foreach (var arrow in _arrowsToCheck)
            {
                var arrowPosition = arrow.transform.position;

                var distance = Vector2.Distance(arrowPosition, transform.position);

                if (distance <= _uiArrowBoundSize)
                {
                    boundArrow = arrow;
                    
                    return true;
                }
            }

            boundArrow = null;
            
            return false;
        }

        public void AddArrowToVision(MusicArrow arrow)
        {
            _arrowsToCheck.Add(arrow);
        }

        public void RemoveArrowFromVision(MusicArrow arrow)
        {
            _arrowsToCheck.Remove(arrow);
        }
    }
}