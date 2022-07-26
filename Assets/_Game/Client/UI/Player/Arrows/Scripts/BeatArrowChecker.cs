using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class BeatArrowChecker : MonoBehaviour
    {
        [SerializeField] private float _uiArrowBoundSize = 0.3f;

        private readonly List<BeatArrow> _arrowsToCheck = new List<BeatArrow>();

        public bool IsArrowInbound(out BeatArrow boundArrow)
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

        public void AddArrowToVision(BeatArrow arrow)
        {
            _arrowsToCheck.Add(arrow);
        }

        public void RemoveArrowFromVision(BeatArrow arrow)
        {
            _arrowsToCheck.Remove(arrow);
        }
    }
}