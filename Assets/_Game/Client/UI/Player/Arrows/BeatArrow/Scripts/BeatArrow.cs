using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(BeatArrowChecker))]
    public class BeatArrow : MonoBehaviour
    {
        public BeatArrowChecker Checker { get; private set; }

        private const float MOVE_SPEED_MULTIPLIER = 10f; //Просто для удобства, чтобы не приходилось указывать огромные числа в редакторе
        
        [SerializeField] private float _moveSpeed;

        private void Awake()
        {
            Checker = GetComponent<BeatArrowChecker>();
        }

        private void Update()
        {
            transform.localPosition += Vector3.up * (_moveSpeed * MOVE_SPEED_MULTIPLIER * Time.deltaTime);
        }
        
        public bool IsInTargetUIArrowBound()
        {
            return Checker.IsInTargetUIArrowBound();
        }
    }
}