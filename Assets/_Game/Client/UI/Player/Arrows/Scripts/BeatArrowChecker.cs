using UnityEngine;

namespace LOK1game
{
    public class BeatArrowChecker : MonoBehaviour
    {
        internal Vector3 DestroyPosition;
        internal Transform CurrentUIArrow;

        public bool CheckDestroyOrNot()
        {
            // Игрок нажал успешно и стрелка удалилась или игрок не успел нажать и стрелка уже улетела и удалилась
            return Vector2.Distance(CurrentUIArrow.position, transform.position) < 0.3 
                   || DestroyPosition.y < transform.position.y;
        }
    }
}