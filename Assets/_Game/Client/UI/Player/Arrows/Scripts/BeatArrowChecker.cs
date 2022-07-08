using UnityEngine;

namespace LOK1game
{
    public class BeatArrowChecker : MonoBehaviour
    {
        public BeatArrow CurrentArrow { get; private set; }
        public bool CurrentArrowInBoundary { get; private set; }

        [SerializeField] private Vector2 _boundarySize = Vector2.one;

        private void Update()
        {
            if(CurrentArrow == null) { return; }

            if (IsInBoundary(CurrentArrow.Position))
                CurrentArrowInBoundary = true;
            else
                CurrentArrowInBoundary = false;
        }

        public void SetArrowToCheck(BeatArrow arrow)
        {
            CurrentArrow = arrow;
        }

        private bool IsInBoundary(Vector3 position)
        {
            if(position.y <= transform.position.y + _boundarySize.y && position.y >= transform.position.y - _boundarySize.y)
                return true;

            return false;
        }
    }
}